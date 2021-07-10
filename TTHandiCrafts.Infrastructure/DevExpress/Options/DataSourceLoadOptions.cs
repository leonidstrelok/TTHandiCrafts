using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using TTHandiCrafts.Infrastructure.DevExpress.ModelBinders;

namespace TTHandiCrafts.Infrastructure.DevExpress.Options
{
    [ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
    public class DataSourceLoadOptions : DataSourceLoadOptionsBase
    {
    }
}