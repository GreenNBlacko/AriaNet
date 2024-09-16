using Aria_Net.IO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria_Net.DB.Classes {
	public abstract class BaseTable(MySqlConnection connection) {
		private MySqlConnection _connection = connection;

		private async Task OpenConnection() {
			await _connection.OpenAsync();
		}

		private async Task CloseConnection() {
			await _connection.CloseAsync();
		}

		protected async Task<Dictionary<string, object>> Select(Func<QueryBuilder, QueryBuilder> queryConfigurator) {
			await OpenConnection();

			var queryBuilder = new QueryBuilder(_connection);
			queryConfigurator(queryBuilder);
			var response = await queryBuilder.ExecuteSelect();

			await CloseConnection();
			return response; 
		}

		protected async Task<bool> Exists(Func<QueryBuilder, QueryBuilder> queryConfigurator) {
			await OpenConnection();

			var queryBuilder = new QueryBuilder(_connection);
			queryConfigurator(queryBuilder);
			var response = await queryBuilder.ExecuteExists();

			await CloseConnection();
			return response;
		}

		protected async Task<object> InsertOrUpdate(Func<QueryBuilder, QueryBuilder> queryConfigurator) {
			await OpenConnection();

			var queryBuilder = new QueryBuilder(_connection);
			queryConfigurator(queryBuilder);
			var response = await queryBuilder.ExecuteInsertOrUpdate();

			await CloseConnection();
			return response;
		}

		protected async Task<int> Delete(Func<QueryBuilder, QueryBuilder> queryConfigurator) {
			await OpenConnection();

			var queryBuilder = new QueryBuilder(_connection);
			queryConfigurator(queryBuilder);
			var response = await queryBuilder.ExecuteDelete();

			await CloseConnection();
			return response;
		}

		protected class QueryBuilder {
			private readonly MySqlConnection _connection;
			private StringBuilder _selectClause;
			private StringBuilder _fromClause;
			private StringBuilder _whereClause;
			private StringBuilder _insertIntoClause;
			private StringBuilder _setClause;
			private StringBuilder _deleteFromClause;
			private Dictionary<string, object> _parameters;

			public QueryBuilder(MySqlConnection connection) {
				_connection = connection;
				_selectClause = new StringBuilder();
				_fromClause = new StringBuilder();
				_whereClause = new StringBuilder();
				_insertIntoClause = new StringBuilder();
				_setClause = new StringBuilder();
				_deleteFromClause = new StringBuilder();
				_parameters = new Dictionary<string, object>();
			}

			public QueryBuilder SetSelect(params string[] selectableValues) {
				if (selectableValues.Length == 0)
					throw new ArgumentException("At least one value must be specified in the SELECT clause.");

				_selectClause.Append("SELECT ");
				_selectClause.Append(string.Join(", ", selectableValues));
				return this;
			}

			public QueryBuilder SetFrom(string tableName) {
				_fromClause.Append($"FROM {tableName} ");
				return this;
			}

			public QueryBuilder SetWhere(params (string name, object value)[] filters) {
				if (filters.Length == 0)
					return this;

				_whereClause.Append("WHERE ");
				var conditions = new List<string>();
				foreach (var filter in filters) {
					conditions.Add($"{filter.name} = @{filter.name}");
					_parameters.Add(filter.name, filter.value);
				}
				_whereClause.Append(string.Join(" AND ", conditions));
				return this;
			}

			public QueryBuilder SetInsertInto(string tableName, params (string name, object value)[] values) {
				if (values.Length == 0)
					throw new ArgumentException("At least one value must be specified for the INSERT INTO clause.");

				_insertIntoClause.Append($"INSERT INTO {tableName} (");
				_insertIntoClause.Append(string.Join(", ", values.Select(v => v.name)));
				_insertIntoClause.Append(") VALUES (");
				_insertIntoClause.Append(string.Join(", ", values.Select(v => $"@{v.name}")));
				_insertIntoClause.Append(") ");

				foreach (var value in values) {
					_parameters.Add(value.name, value.value);
				}

				return this;
			}

			public QueryBuilder SetUpdate(string tableName, params (string name, object value)[] updates) {
				if (updates.Length == 0)
					throw new ArgumentException("At least one value must be specified for the UPDATE clause.");

				_setClause.Append($"UPDATE {tableName} SET ");
				var updatesList = new List<string>();
				foreach (var update in updates) {
					updatesList.Add($"{update.name} = @{update.name}");
					_parameters.Add(update.name, update.value);
				}
				_setClause.Append(string.Join(", ", updatesList));
				return this;
			}

			public QueryBuilder SetDeleteFrom(string tableName) {
				_deleteFromClause.Append($"DELETE FROM {tableName} ");
				return this;
			}

			public async Task<Dictionary<string, object>> ExecuteSelect() {
				var query = new StringBuilder();
				query.Append(_selectClause);
				query.Append(" ");
				query.Append(_fromClause);
				if (_whereClause.Length > 0) {
					query.Append(" ");
					query.Append(_whereClause);
				}

				using var command = new MySqlCommand(query.ToString(), _connection);
				foreach (var parameter in _parameters) {
					command.Parameters.AddWithValue(parameter.Key, parameter.Value);
				}

				var result = new Dictionary<string, object>();

				using (var reader = await command.ExecuteReaderAsync()) {
					while (await reader.ReadAsync()) {
						for (int i = 0; i < reader.FieldCount; i++) {
							if(result.ContainsKey(reader.GetName(i))) {
								if (result[reader.GetName(i)] is object[]) {
									var temp = new List<object>(result[reader.GetName(i)] as object[]) {
										reader.GetValue(i)
									};

									result[reader.GetName(i)] = temp.ToArray();
									continue;
								}

								var list = new List<object> {
									result[reader.GetName(i)],
									reader.GetValue(i)
								};
								result[reader.GetName(i)] = list.ToArray();
								continue;
							}

							result[reader.GetName(i)] = reader.GetValue(i);
						}
					}
				}

				return result;
			}

			public async Task<bool> ExecuteExists() {
				var query = new StringBuilder();
				query.Append(_selectClause);
				query.Append(" ");
				query.Append(_fromClause);
				if (_whereClause.Length > 0) {
					query.Append(" ");
					query.Append(_whereClause);
				}

				using var command = new MySqlCommand(query.ToString(), _connection);
				foreach (var parameter in _parameters) {
					command.Parameters.AddWithValue(parameter.Key, parameter.Value);
				}

				using var reader = await command.ExecuteReaderAsync();

				return reader.HasRows;
			}

			public async Task<object> ExecuteInsertOrUpdate() {
				var query = new StringBuilder();
				if (_insertIntoClause.Length > 0) {
					query.Append(_insertIntoClause);
				} else if (_setClause.Length > 0) {
					query.Append(_setClause);
					if (_whereClause.Length > 0) {
						query.Append(" ");
						query.Append(_whereClause);
					}
				} else {
					throw new InvalidOperationException("No INSERT or UPDATE operation defined.");
				}

				using var command = new MySqlCommand(query.ToString(), _connection);
				foreach (var parameter in _parameters) {
					command.Parameters.AddWithValue(parameter.Key, parameter.Value);
				}

				await command.ExecuteNonQueryAsync();
				return command.LastInsertedId;
			}

			public async Task<int> ExecuteDelete() {
				if (_deleteFromClause.Length == 0 || _whereClause.Length == 0) {
					throw new InvalidOperationException("DELETE operation requires both DELETE FROM and WHERE clauses.");
				}

				var query = new StringBuilder();
				query.Append(_deleteFromClause);
				query.Append(" ");
				query.Append(_whereClause);

				using var command = new MySqlCommand(query.ToString(), _connection);
				foreach (var parameter in _parameters) {
					command.Parameters.AddWithValue(parameter.Key, parameter.Value);
				}

				return await command.ExecuteNonQueryAsync();
			}
		}
	}
}
