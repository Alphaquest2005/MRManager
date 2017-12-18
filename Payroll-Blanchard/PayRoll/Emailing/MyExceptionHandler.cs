using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;


namespace EmailLogger
{
    [Serializable]
    public class MyExceptionHandlerAspect : OnExceptionAspect
    {
        static EmailslNotifications en = new EmailslNotifications("Exception Occured");

        public override void OnException(MethodExecutionArgs args)
        {
            string argstr = "";
            //foreach (var a in args.Arguments)
            //{
            //   argstr += string.Format( " ParameterName:{0} | Value:{1} \r\n", a.GetType().Name, a.ToString());
            //}

            en.SendNotificationEmail("",
                Properties.Settings.Default.MachineName + ".com",
                Properties.Settings.Default.MachineName,
                @"logs@insight-software.biz",
                null,
                "Exception Occured",
                args.Method.Name, argstr, args.Exception.GetType().Name, args.Exception.Message, args.Exception.StackTrace);


        }
    }
}
