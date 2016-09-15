﻿using System.ComponentModel.DataAnnotations;

namespace WarmBeer.Web.Models
{
    public class SuggestionModel
    {

        public SuggestionModel()
        {
            this.Radius = 10000;
            this.HighestAlcohol = false;
        }

        [Range(11, 25)]
        public decimal Longitude { get; set; }

        [Range(55, 70)]
        public decimal Latitude { get; set; }

        [Range(100, 500000)]
        public int Radius { get; set; }

        public bool HighestAlcohol { get; set; }
    }
}