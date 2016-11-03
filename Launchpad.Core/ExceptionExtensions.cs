﻿using System;
using System.Linq;
using System.Text;

namespace Launchpad.Core
{
    public static class ExceptionExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string FlattenMessages(this AggregateException exception)
        {
            const int maxDepth = 5;
            int depth = 0;

            StringBuilder sb = new StringBuilder();

            Exception ex = exception;
            
            while(ex != null && depth < maxDepth)
            {
                sb.AppendLine(ex.Message);
                ex = ex.InnerException;
                ++depth;
            }
            
            return sb.ToString();
        }
    }
}