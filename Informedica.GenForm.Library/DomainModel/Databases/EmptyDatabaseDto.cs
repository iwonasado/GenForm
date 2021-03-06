using System;
using Informedica.GenForm.Library.DomainModel.Data;

namespace Informedica.GenForm.Library.DomainModel.Databases
{
    public class EmptyDatabaseDto: DataTransferObject<EmptyDatabaseDto>
    {
        public Boolean IsEmpty;

        public override EmptyDatabaseDto CloneDto()
        {
            return CreateClone();
        }
    }
}