﻿using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class SubstanceMap: EntityMap<Substance>
    {
        public SubstanceMap()
        {
            References(x => x.SubstanceGroup)
                .Cascade.SaveUpdate();
        }
    }
}
