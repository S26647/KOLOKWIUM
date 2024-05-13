using System.Data.Common;
using System.Data.SqlClient;
using KOLOKWIUM.DTO;
using Microsoft.AspNetCore.Components.Web;

namespace KOLOKWIUM.Repos;

public class FireActionRepo : IFireActionRepo
{
    private IConfiguration _configuration;

    public FireActionRepo(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<FireFighterActionDTO>> GetFireActionByFireFighterIdAsync(int id)
    {
        using SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM FireAction " +
                          "JOIN Firefighter_Action ON FireAction.IdFireAction = Firefighter_Action.IdFireAction " +
                          "WHERE Firefighter_Action.IdFirefighter = @IdFirefighter " +
                          "ORDER BY FireAction.EndTime DESC";
        cmd.Parameters.AddWithValue("@IdFirefighter", id);

        var actionList = new List<FireFighterActionDTO>();

        using (var dr = await cmd.ExecuteReaderAsync())
        {
            while (await dr.ReadAsync())
            {
                int idFireAction = Int32.Parse(dr["IdFireAction"].ToString());
                DateTime starTime = Convert.ToDateTime(dr["StartTime"].ToString());
                string? endTimetmp = dr["EndTime"].ToString();

                DateTime? endTime;
                if (endTimetmp == "")
                {
                    endTime = null;
                }
                else
                {
                    endTime = Convert.ToDateTime(dr["EndTime"].ToString());
                }

                FireFighterActionDTO firefighterdto = new FireFighterActionDTO(idFireAction, starTime, endTime);

                actionList.Add(firefighterdto);
            }
        }

        return actionList;
    }

    public async Task AssignFireTruckToActionAsync(int idFireAction, int idFireTruck)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        
        String dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        
        DbTransaction transaction = await con.BeginTransactionAsync();
        cmd.Transaction = (SqlTransaction)transaction;

        try
        {
            cmd.CommandText = "INSERT INTO FireTruck_Action VALUES (@IdFireTruck,@IdFireAction,@AssignmentDate);";
            cmd.Parameters.AddWithValue("@AssignmentDate", DateTime.Now.ToString(dateTimeFormat));
            cmd.Parameters.AddWithValue("@IdFireAction", idFireAction);
            cmd.Parameters.AddWithValue("@IdFireTruck", idFireTruck);

            await cmd.ExecuteNonQueryAsync();
            
        }
        catch (SqlException ex)
        {
            Console.WriteLine("error in transaction");
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> CheckIfFireTruckTakenAsync(int idFireTruck)
    {
        using SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM FireTruck_Action " +
                          "WHERE FireTruck_Action.IdFireTruck = @IdFireTruck";
        cmd.Parameters.AddWithValue("@IdFireTruck", idFireTruck);

        var dr = await cmd.ExecuteReaderAsync();
        await dr.ReadAsync();

        string id = dr["IdFireAction"].ToString();
        if (id == "")
        {
            return false;
        }

        return true;
    }

    public async Task<bool> CheckIfFireTruckNeedsSpecialEqAsync(int idFireTruck)
    {
        using SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT SpecialEquipment FROM FireTruck " +
                          "WHERE FireTruck.IdFireTruck = @IdFiretruck";
        cmd.Parameters.AddWithValue("@IdFiretruck", idFireTruck);

        var actionList = new List<FireFighterActionDTO>();
        var dr = await cmd.ExecuteReaderAsync();
        await dr.ReadAsync();

        bool needEq = Convert.ToBoolean(dr["SpecialEquipment"].ToString());

        await dr.CloseAsync();

        return needEq;
    }

    public async Task<bool> CheckIfActionNeedsSpecialEqAsync(int idFireAction)
    {
        using SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT NeedSpecialEquipment FROM FireAction " +
                          "WHERE FireAction.IdFireAction = @IdFireAction";
        cmd.Parameters.AddWithValue("@IdFireAction", idFireAction);

        var actionList = new List<FireFighterActionDTO>();
        var dr = await cmd.ExecuteReaderAsync();
        await dr.ReadAsync();

        bool needEq = Convert.ToBoolean(dr["NeedSpecialEquipment"].ToString());
        
        await dr.CloseAsync();

        return needEq;
    }
}