using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Elastic
{
    public class EsResponce<T>
    {
        public string _scroll_id { get; set; }
        public int took { get; set; }
        public bool timed_out { get; set; }
        public root<T> hits { get; set; }
    }

    public class root<T>
    {
        public Total total { get; set; }
        public float? max_score { get; set; }
        public Hits<T>[] hits { get; set; }
    }

    public class Total
    {
        public int value { get; set; }
        public string relation { get; set; }
    }
    public class Hits<T>
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public float? _score { get; set; }
        public T _source { get; set; }
        public string[] sort { get; set; }
    }
}
