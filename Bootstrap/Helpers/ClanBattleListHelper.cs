using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bootstrap.Models;

namespace Bootstrap.Helpers

{
    public class ClanBattleListHelper
    {
        static public List<string> GetListofClanBattlesFromCompany(string company)
        {
            List<string> battleList = new List<string>();
            battleList.Clear(); //Make list list so we dont return a null list 

            try
            {
                using (var db = new BattleModel())
                {

                    var query = from cb in db.v_Battle
                                where cb.a_company == company || cb.b_company == company
                                select cb;

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            battleList.Add(item.matchID.ToString());
                        }
                        return battleList; //GOOD PLACE FOR A BREAKPOINT
                    }
                    else
                    {
                        return battleList;
                    }
                
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("QUERY EXCEPTION HAPPENED HERE");
                return battleList;
            }

        }
    }
}