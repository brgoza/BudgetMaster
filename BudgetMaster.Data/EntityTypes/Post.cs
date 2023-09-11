using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetMaster.Data.EntityTypes.Identity;

namespace BudgetMaster.Data.EntityTypes;

public class Post
{
    public Guid Id { get; set; }
    public AppUser Author { get; set; } = null!;
    public DateTime Created { get; set; }
    public string Text { get; set; } = null!;

}
