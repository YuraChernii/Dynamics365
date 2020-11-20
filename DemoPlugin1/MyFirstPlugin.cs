using Microsoft.Xrm.Sdk;
using System.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DemoPlugin1
{
   public class MyFirstPlugin: IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {


            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.MessageName != "Create") { return; }

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];

                if (entity.LogicalName != "account") { return; }

                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {
                    if (context.Stage != 40)
                    {
                        Entity updateAccount = new Entity("account");
                        updateAccount.Id = entity.Id;
                        updateAccount.Attributes["description"] = "Hello World";
                        service.Update(updateAccount);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
