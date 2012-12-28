using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acrobat;

namespace FaxHandler.PDF
{ // overview
    // we get a file from explorer/mozilla or outlook.  outlook files will be temp files, others will not
    // either way, we need a temp file.  so, copy explorer/mozilla to temp file to keep same operations.
    // take the temp file, make a new PDDoc which will be anonymous.
    // when extracting, it will be from the anonymous pddoc to another temp file, which will then be moved/copied
    // to the ultimate destination.  whew!
    class Document// : IDisposable
    { 
        AcroAVDoc document = null;
        
    }
}
