using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ToDoList.Dal.Entities;
using ToDoList.Repository.Settings;

namespace ToDoList.Repository.Services;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly string _connectionString;

    public ToDoItemRepository(SqlDBConeectionString sqlDBConeectionString)
    {
        _connectionString = sqlDBConeectionString.ConnectionString;
    }

    public async Task DeleteToDoItemByIdAsync(long id)
    {
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_DeleteToDoItemById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<long> InsertToDoItemAsync(ToDoItem toDoItem)
    {
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_AddToDoItem", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", toDoItem.Title);
                cmd.Parameters.AddWithValue("@Description", (object?)toDoItem.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsCompleted", toDoItem.IsCompleted);
                cmd.Parameters.AddWithValue("@CreatedAt", toDoItem.CreatedAt);
                cmd.Parameters.AddWithValue("@DueDate", toDoItem.DueDate);

                var insertedId = await cmd.ExecuteScalarAsync();
                return Convert.ToInt64(insertedId);
            }
        }
    }

    public async Task<List<ToDoItem>> SelectAllToDoItemsAsync(int skip, int take)
    {
        var result = new List<ToDoItem>();

        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_GetAllToDoItems", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Skip", skip);
                cmd.Parameters.AddWithValue("@Take", take);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(MapToDoItem(reader));
                    }
                }
            }
        }

        return result;
    }

    public async Task<List<ToDoItem>> SelectByDueDateAsync(DateTime dateTime)
    {
        var result = new List<ToDoItem>();

        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_GetByDueDate", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DueDate", dateTime.Date);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(MapToDoItem(reader));
                    }
                }
            }
        }

        return result;
    }

    public async Task<List<ToDoItem>> SelectCompletedAsync(int skip, int take)
    {
        var result = new List<ToDoItem>();

        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_GetCompletedItems", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Skip", skip);
                cmd.Parameters.AddWithValue("@Take", take);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(MapToDoItem(reader));
                    }
                }
            }
        }

        return result;
    }

    public async Task<List<ToDoItem>> SelectIncompleteAsync(int skip, int take)
    {
        var result = new List<ToDoItem>();

        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_GetIncompleteItems", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Skip", skip);
                cmd.Parameters.AddWithValue("@Take", take);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(MapToDoItem(reader));
                    }
                }
            }
        }

        return result;
    }

    public async Task<ToDoItem> SelectToDoItemByIdAsync(long id)
    {
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_GetToDoItemById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapToDoItem(reader);
                    }
                }
            }
        }

        return null;
    }

    public int SelectToDoListCount()
    {
        var Counts = 0;
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            using (var cmd = new SqlCommand("SELECT dbo.CountOfToDoLists()", conn))
            {
                cmd.CommandType = CommandType.Text;
                Counts = (int)cmd.ExecuteScalar();
            }
        }
        return Counts;
    }

    public async Task UpdateToDoItemAsync(ToDoItem updatedToDoItem)
    {
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_UpdateToDoItem", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", updatedToDoItem.ToDoItemId);
                cmd.Parameters.AddWithValue("@Title", updatedToDoItem.Title);
                cmd.Parameters.AddWithValue("@Description", (object?)updatedToDoItem.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsCompleted", updatedToDoItem.IsCompleted);
                cmd.Parameters.AddWithValue("@DueDate", updatedToDoItem.DueDate);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    private ToDoItem MapToDoItem(SqlDataReader reader)
    {
        return new ToDoItem
        {
            ToDoItemId = reader.GetInt64(reader.GetOrdinal("Id")),
            Title = reader.GetString(reader.GetOrdinal("Title")),
            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
            IsCompleted = reader.GetBoolean(reader.GetOrdinal("IsCompleted")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate"))
        };
    }
}
