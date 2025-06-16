﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public String Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public DifficultyDto Difficulty { get; set; }


        public RegionDto Region { get; set; }
    }
}
