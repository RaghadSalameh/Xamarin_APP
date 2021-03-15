using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace Xamarin_App.Models
{
    public class Field
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FieldName { get; set; }
        
        [OneToMany]
        public List<Coordinate> coordinates { get; set; }
    }

    public class Coordinate
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Field))]
        public int fieldId { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }

        [OneToMany]
        public Field field { get; set; }
    }
    //public class Field
    //{
    //    [PrimaryKey,AutoIncrement]
    //    public int Id { get; set; }
    //    public string FieldName { get; set; }
    //    public string type { get; set; }
    //    public bool fallback_boundary { get; set; }

    //    [OneToMany]
    //    public lFeature[] features { get; set; }
    //}

    //public class lFeature
    //{
    //    [PrimaryKey,AutoIncrement]
    //    public int id { get; set; }

    //    [ForeignKey(typeof(Field))]
    //    public int FieldId {get; set;}
    //    public string type { get; set; }

    //    [ManyToOne]
    //    public Field field { get; set; }

    //    [OneToOne]
    //    public lGeometry geometry { get; set; }

    //    [OneToOne]
    //    public lProperties properties { get; set; }
    //}

    //public class lGeometry
    //{
    //    [PrimaryKey,AutoIncrement]
    //    public int id { get; set; }

    //    [ForeignKey(typeof(lFeature))]
    //    public int Featureid { get; set; }
    //    public string type { get; set; }
    //    public double[][][] coordinates { get; set; }

    //    [OneToOne]
    //    public lFeature feature { get; set; }
    //}

    //public class lProperties
    //{
    //    [PrimaryKey, AutoIncrement]
    //    public int id { get; set; }

    //    [ForeignKey(typeof(lFeature))]
    //    public int Featureid { get; set; }

    //    public string status { get; set; }
    //    public object corrected { get; set; }
    //    public string schema_id { get; set; }
    //    public float area { get; set; }
    //    public string fb_type { get; set; }
    //    public string operation_status { get; set; }
    //    public string report_id { get; set; }
    //    public DateTime created_date { get; set; }
    //    public DateTime updated_date { get; set; }

    //    [OneToOne]
    //    public lFeature feature { get; set; }
    //}
}
