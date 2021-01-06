using System;
using System.Collections.Generic;
using Domain.Infra.Generics;

namespace Domain.ClassDays
{
    public class CreatedClassDayDTO : CreatedEntityDTO
    {
        public CreatedClassDayDTO(Guid id) : base(id)
        {}
        public CreatedClassDayDTO(List<string> errors) : base(errors)
        {}
        public CreatedClassDayDTO(string error) : base(error)
        {}
    }
}