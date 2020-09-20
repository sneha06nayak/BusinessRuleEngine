﻿namespace RulesEngine.Common.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public RuleType ProductType { get; set; }
    }
}