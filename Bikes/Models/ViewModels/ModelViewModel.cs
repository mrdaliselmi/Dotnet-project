using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bikes.Models.ViewModels
{
    public class ModelViewModel
    {
        public Model Model { get; set; }
        public IEnumerable<Make> Makes { get; set; }

        public IEnumerable<SelectListItem> CSelectListITem(IEnumerable<Make> Items)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem
            {
                Text = "----Select----",
                Value = "0",
            };
            list.Add(sli);
            foreach (Make make in Items)
            {
                SelectListItem sl = new SelectListItem
                {
                    Text = make.Name,
                    Value = make.Id.ToString()
                };
                list.Add(sl);
            }
            return list;
        }
    }
}
