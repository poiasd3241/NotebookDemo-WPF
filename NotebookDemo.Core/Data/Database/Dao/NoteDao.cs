using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using NotebookDemo.Core.Data.Database.Entity;

namespace NotebookDemo.Core.Data.Database.Dao
{
	/// <summary>
	/// Default implementation of <see cref="INoteDao"/>.
	/// </summary>
	public class NoteDao : INoteDao
	{
		#region Private Members

		private readonly string _connectionString;

		#endregion

		#region Constructor

		public NoteDao(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");
		}

		#endregion

		#region Public Methods

		public async Task<NoteDbEntity> Create(NoteDbEntity note)
		{
			var sql = "INSERT INTO Notes (text, important) values (@Text, @Important);" +
				"SELECT id, text, important from Notes WHERE id = last_insert_rowid();";
			using IDbConnection connection = GetSqliteConnection();
			var createdNote = await connection.QuerySingleAsync<NoteDbEntity>(sql, note);
			return createdNote;
		}

		public async Task Update(NoteDbEntity note)
		{
			var sql = "UPDATE Notes SET text = @Text, important = @Important WHERE id = @ID";
			using IDbConnection connection = GetSqliteConnection();
			await connection.ExecuteAsync(sql, note);
		}

		public async Task Delete(int id)
		{
			var sql = "DELETE FROM Notes WHERE id = @ID";
			using IDbConnection connection = GetSqliteConnection();
			await connection.ExecuteAsync(sql, new { ID = id });
		}

		public async Task<IEnumerable<NoteDbEntity>> GetAll()
		{
			var sql = "SELECT id, text, important from Notes";
			using IDbConnection connection = GetSqliteConnection();
			var notes = await connection.QueryAsync<NoteDbEntity>(sql);
			return notes;
		}

		#endregion

		#region Private Methods

		/// <returns>A new <see cref="SQLiteConnection"/> instance.</returns>
		private IDbConnection GetSqliteConnection() => new SQLiteConnection(_connectionString);

		#endregion
	}
}
