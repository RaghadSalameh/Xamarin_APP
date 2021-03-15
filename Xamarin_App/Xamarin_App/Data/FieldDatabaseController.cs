using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin_App.Models;
using SQLiteNetExtensions.Extensions;

namespace Xamarin_App.Data
{
    public class FieldDatabaseController
    {
        SQLiteConnection database;

        public FieldDatabaseController()
        {
            database = DependencyService.Get<ISQLiteDb>().GetConnection();
            database.CreateTable<Field>();
            database.CreateTable<Coordinate>();
        }

        public List<Field> GetFields()
        {
            return database.Table<Field>().ToList();
        }

        public List<Field> GetSearchedFields(string str)
        {
            // SQL queries are also possible
            //return database.Query<Field>("SELECT * FROM [Field] WHERE [Done] = 0");
            return database.Table<Field>().Where(i => i.FieldName.Contains(str)).ToList();
        }

        public Field GetField(int id)
        {
            return database.Table<Field>().Where(i => i.Id == id).FirstOrDefault();
        }

        public int SaveField(Field Field)
        {
            if (Field.Id != 0)
            {
                return database.Update(Field);
            }
            else
            {
                return database.Insert(Field);
            }
        }

        public int DeleteField(Field Field)
        {
            return database.Delete(Field);
        }

        public int SaveCoordinate(Coordinate coordinate)
        {
            if (coordinate.Id != 0)
            {
                return database.Update(coordinate);
            }
            else
            {
                return database.Insert(coordinate);
            }
        }
        public List<Coordinate> GetCoordinate(int fieldId)
        {
            return database.Query<Coordinate>("SELECT * FROM [Coordinate] WHERE [fieldId] ="+fieldId);
        }

        public int DeleteCoordinate(Coordinate coordinate)
        {
            return database.Delete(coordinate);
        }

        public void DeleteFieldWithitsCoordinate(int fieldId)
        {
            List<Coordinate> cs = GetCoordinate(fieldId);
            foreach(Coordinate c in cs )
            {
                DeleteCoordinate(c);
            }
            Field f = GetField(fieldId);
            DeleteField(f);
        }
    }
}
