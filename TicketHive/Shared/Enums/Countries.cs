﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TicketHive.Shared.Enums;

public enum Countries
{
    Austria,
    Belgium,
    Bulgaria,
    Croatia,
    [Display(Name = "Republic of Cyprus")]
    Republic_of_Cyprus,
    [Display(Name = "Czech Republic")]
    Czech_Republic,
    Denmark,
    Estonia,
    Finland,
    France,
    Germany,
    [Display(Name = "Great Britain")]
    Great_Britain,
    Greece,
    Hungary,
    Ireland,
    Italy,
    Latvia,
    Lithuania,
    Luxembourg,
    Malta,
    Netherlands,
    Poland,
    Portugal,
    Romania,
    Slovakia,
    Slovenia,
    Spain,
    Sweden
}
