﻿using System;

namespace DAL.Entities
{
    public class NewsEntity
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime NewsDate { get; set; }
    }
}
