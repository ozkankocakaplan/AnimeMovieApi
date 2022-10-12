using System;
namespace AnimeMovie.Entites
{
    public class HomeSlider : BaseEntity
    {
        public string Image { get; set; }
        public string SliderTitle { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool isDisplay { get; set; }
    }
}

