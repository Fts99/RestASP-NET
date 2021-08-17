using System.Collections.Generic;

namespace RestASPNET.Data.Converter.Contract
{
    interface Iparser<O,D>
    {
        D Parse(O origin);

        List<D> Parse(List<O> origin);
    }
}
