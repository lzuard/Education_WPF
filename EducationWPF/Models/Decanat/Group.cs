﻿using System.Collections.Generic;

namespace EducationWPF.Models.Decanat;

internal class Group
{
    public string Name { get; set; }

    public IList<Student> Students { get; set; }

    public string Description { get; set; }
}