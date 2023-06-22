﻿namespace RestApiDDD.Domain.Entities
{
    public class Product : Base
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsAvaiable { get; set; }
    }
}
