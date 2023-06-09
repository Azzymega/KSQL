﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace KSQL.Scripts
{
    public class DataReader
    {
        SQLiteDataReader reader;
        public DataReader(SQLiteCommand command, Database dbase)
        {
            try
            {
                reader = command.ExecuteReader();
            }
            catch
            {
                dbase.ChangeStatus(EStatus.READ_ERROR);
            }
        }
        public List<string> Read()
        {
            List<string> list = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(1));
                }
            }
            return list;
        }
    }
}
