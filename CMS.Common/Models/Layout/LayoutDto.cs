using CMS.Common.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common.Models.Layout
{
    public class LayoutDto:BaseDto
    {
        public LayoutDto()
        {
            this.Items = new List<PLItemDto>();
        }
        public string Name { get; set; }
        public IEnumerable<PLItemDto> Items { get; set; }
    }
    public class PLItemDto : BaseDto
    {
        public PLItemDto()
        {
            Sizes = new List<ParameterDto>()
            {
                new ParameterDto{Name="Small",Value="col-sm"},
                new ParameterDto{Name="Medium",Value="col-md"},
                new ParameterDto{Name="Large",Value="col-lg"},

            };
            
    }
        public IEnumerable<ParameterDto> Sizes { get; set; }
        public int SelectedColumn { get; set; }
        public string SizeValue { get; set; }
        public int Order { get; set; }
        public string Size { get;/*{ return string.Format("{0}-{1}", this.SizeValue, this.SelectedColumn);}*/ set; }

    }
}
