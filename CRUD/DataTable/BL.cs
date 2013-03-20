using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BL
/// </summary>
/// 
namespace BL
{
    public class CreateDataTable
    {
        public static DataTable CreateDataTabelMethod(int length)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < length; i++)
            {
                dt.Columns.Add();
            }
            return dt;
        }
    }
}