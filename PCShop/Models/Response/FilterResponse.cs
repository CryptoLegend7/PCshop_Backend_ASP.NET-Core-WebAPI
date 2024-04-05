using Microsoft.AspNetCore.Mvc;

namespace PCShop.Models.Response
{
    public class FilterResponse
    {
        public int ID { get; set; }
        public string Memory { get; set; }
        public string Storage { get; set; }
        public string Port { get; set; }
        public string Render { get; set; }
        public string Weight { get; set; }
        public string PSU { get; set; }
        public string Processor { get; set; }
        public JsonResult toJSON()
        {
            var data = new
            {
                ID = ID,
                Memory = Memory,
                Storage = Storage,
                Port = Port,
                Render = Render,
                Weight = Weight,
                PSU = PSU,
                Processor = Processor
            };
            return new JsonResult(data);
        }
        public bool contains(string keyword)
        {
            if (Memory.Contains(keyword, StringComparison.OrdinalIgnoreCase) || Storage.Contains(keyword, StringComparison.OrdinalIgnoreCase) || 
                Port.Contains(keyword, StringComparison.OrdinalIgnoreCase) || Render.Contains(keyword, StringComparison.OrdinalIgnoreCase) || 
                Weight.Contains(keyword, StringComparison.OrdinalIgnoreCase) || PSU.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                Processor.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}
