using GraphQL.Language.AST;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog.Types
{
    /// <summary>
    /// The way to provide a DateTime type not supported natively by GraphQL.
    /// How is it actually implemented?
    /// </summary>
    public class Date : ScalarGraphType
    {
        public override object ParseLiteral(IValue value)
        {
            throw new NotImplementedException();
        }

        public override object ParseValue(object value)
        {
            throw new NotImplementedException();
        }

        public override object Serialize(object value)
        {
            throw new NotImplementedException();
        }
    }
}
