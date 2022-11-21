﻿namespace NZWalk.API.Models.DTO
{
    public class UpdateRegionRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public long Population { get; set; }
    }
}
