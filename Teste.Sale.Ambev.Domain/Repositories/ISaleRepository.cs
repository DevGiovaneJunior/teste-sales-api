using System;
using System.Collections.Generic;
using Teste.Sale.Ambev.Domain.Entities;


namespace Teste.Sale.Ambev.Domain.Repositories
{
    public interface ISaleRepository
    {
        SaleEntity? GetById(Guid id);
        SaleItemEntity? GetItemById(Guid SaleItemId);
        IEnumerable<SaleEntity> GetAll();
        void Save(SaleEntity sale);
        void Update(SaleEntity sale);
    }
}
