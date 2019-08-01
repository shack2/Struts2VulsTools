using System;
using System.Collections.Generic;
using System.Text;

namespace payload
{
   public interface BasePayload
    {
        String Get_Exp_VerInfo(String vername);
        String Get_Exp_Check();
        String Get_Exp_Path();
        String Get_Exp_Exec(String cmd);

        String Get_Exp_Upload(String path, String fileName, String fileContent);
        
    }
}
