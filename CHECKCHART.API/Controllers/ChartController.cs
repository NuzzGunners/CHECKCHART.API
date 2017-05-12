using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using CHECKCHART.API.Abstract;
using Microsoft.Extensions.Configuration;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class ChartController : Controller
    {
        public static IConfigurationRoot Configuration { get; set; }

        public string DefaultConnection()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            return Configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("receivechart")]
        public IActionResult GetCheckchart(string df, string dt)
        {
            DateTime d1 = DateTime.Parse(string.Format("{0:yyyy-MM-dd 00:00:00}", DateTime.Now));
            DateTime d2 = DateTime.Parse(string.Format("{0:yyyy-MM-dd 23:59:59}", DateTime.Now));

            SqlConnection cn = new SqlConnection(DefaultConnection());
            cn.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = "dbo.SP_GetMonitorChart1";
            cm.Parameters.Add(new SqlParameter("@RptFromdatetime", SqlDbType.DateTime) { Value = d1 });
            cm.Parameters.Add(new SqlParameter("@RptTodatetime", SqlDbType.DateTime) { Value = d2 });
            cm.Connection = cn;

            var dataReader = cm.ExecuteReader();

            List<Entry> listEntry = new List<Entry>();
            while (dataReader.Read())
            {
                Entry obj = new Entry();
                obj.name = Convert.ToString(dataReader["Name"]);
                obj.y = Convert.ToInt32(dataReader["Qty"]);

                listEntry.Insert(0, obj);
            }

            dataReader.Dispose();
            cn.Dispose();
            cn.Close();

            return Ok(listEntry);
        }

        [HttpGet("{df}/{dt}")]
        public IActionResult GetChart1(string df, string dt)
        {
            DateTime d1;
            DateTime d2;

            if (DateTime.Parse(df).Year != DateTime.Now.Year)
            {
                d1 = DateTime.Parse(df).AddYears(543);
                d2 = DateTime.Parse(dt).AddYears(543);
            }
            else
            {
                d1 = DateTime.Parse(df);
                d2 = DateTime.Parse(dt);
            }           

            SqlConnection cn = new SqlConnection(DefaultConnection());
            cn.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = "dbo.SP_GetChart1";
            cm.Parameters.Add(new SqlParameter("@RptFromdatetime", SqlDbType.DateTime) { Value = d1 });
            cm.Parameters.Add(new SqlParameter("@RptTodatetime", SqlDbType.DateTime) { Value = d2 });
            cm.Connection = cn;

            var dataReader = cm.ExecuteReader();

            List<Entry> listEntry = new List<Entry>();
            while (dataReader.Read())
            {
                Entry obj = new Entry();
                obj.name = string.Format("({0}) {1}", Convert.ToString(dataReader["Rk"]), Convert.ToString(dataReader["ActiveWardName"]));
                obj.y = Convert.ToInt32(dataReader["Qty"]);
                obj.n = Convert.ToInt32(dataReader["TotalQty"]);
                obj.p = Convert.ToDecimal(dataReader["Percentage"]);

                listEntry.Insert(0, obj);
            }

            dataReader.Dispose();
            cn.Dispose();
            cn.Close();

            return Ok(listEntry);
        }

        public class Series
        {
            public object data { get; set; }
        }

        public class Entry
        {
            public string name { get; set; }
            public int y { get; set; }
            public int n { get; set; }
            public decimal p { get; set; }
        }
    }
}
