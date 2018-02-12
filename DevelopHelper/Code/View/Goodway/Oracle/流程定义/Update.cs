using System;
using System.Collections.Generic;
using Common;
using DbHelper;

namespace View.Goodway.Oracle
{
    /// <summary>
    /// 更新
    /// </summary>
    class Update
    {
        readonly SqlHelper sqlHelper1 = OracleHelper.CreateSqlHelper("WorkflowObject");
        readonly SqlHelper sqlHelper2 = OracleHelper.CreateSqlHelper("WorkflowSource");

        public void UpdateDB(List<IdAndCode> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                LogWriter.Info(item.Code + " ===========开始更新==========");

                //删除原有流程
                const string sqlDel = "DELETE FROM S_WF_DefFlow WHERE ID='{0}' AND Code='{1}'";
                sqlHelper1.ExecuteNonQuery(String.Format(sqlDel, item.ID, item.Code));

                LogWriter.Info(item.Code + "已删除原流程。");

                //添加流程
                const string sqlFlow = "SELECT * FROM S_WF_DefFlow WHERE ID='{0}' AND Code='{1}'";
                var dtFlow = sqlHelper2.ExecuteDataTable(String.Format(sqlFlow, item.ID, item.Code));
                if (dtFlow.Rows.Count > 0)
                {
                    const string sqlInserFlow = "INSERT INTO S_WF_DefFlow(" +
                                                "ID, " +
                                                "CODE," +
                                                "NAME," +
                                                "CONNNAME," +
                                                "TABLENAME," +
                                                "FORMURL," +
                                                "FORMWIDTH," +
                                                "FORMHEIGHT," +
                                                "FLOWNAMETEMPLETE," +
                                                "FLOWCATEGORYTEMPLETE," +
                                                "FLOWSUBCATEGORYTEMPLETE," +
                                                "TASKNAMETEMPLETE," +
                                                "TASKCATEGORYTEMPLETE," +
                                                "TASKSUBCATEGORYTEMPLETE," +
                                                "INITVARIABLE," +
                                                "ALLOWDELETEFORM," +
                                                "SENDMSGTOAPPLICANT," +
                                                "VIEWCONFIG," +
                                                "MODIFYTIME," +
                                                "CATEGORYID," +
                                                "DESCRIPTION," +
                                                "FORMNUMBERPARTA," +
                                                "FORMNUMBERPARTB," +
                                                "FORMNUMBERPARTC," +
                                                "ALREADYRELEASED" +
                                                ") VALUES(" +
                                                "'{0}'," +
                                                "'{1}'," +
                                                "'{2}'," +
                                                "'{3}'," +
                                                "'{4}'," +
                                                "'{5}'," +
                                                "'{6}'," +
                                                "'{7}'," +
                                                "to_char('{8}')," +
                                                "to_char('{9}')," +
                                                "to_char('{10}')," +
                                                "to_char('{11}')," +
                                                "to_char('{12}')," +
                                                "to_char('{13}')," +
                                                "'{14}'," +
                                                "'{15}'," +
                                                "'{16}'," +
                                                ":clob," +
                                                "to_date('{17}','yyyy-MM-dd HH24:mi:ss')," +
                                                "'{18}'," +
                                                "'{19}'," +
                                                "'{20}'," +
                                                "'{21}'," +
                                                "'{22}'," +
                                                "'{23}')";

                    sqlHelper1.ExecuteWithParam(
                        String.Format(sqlInserFlow, dtFlow.Rows[0]["ID"], dtFlow.Rows[0]["CODE"], dtFlow.Rows[0]["NAME"],
                            dtFlow.Rows[0]["CONNNAME"], dtFlow.Rows[0]["TABLENAME"], dtFlow.Rows[0]["FORMURL"],
                            dtFlow.Rows[0]["FORMWIDTH"],
                            dtFlow.Rows[0]["FORMHEIGHT"], dtFlow.Rows[0]["FLOWNAMETEMPLETE"],
                            dtFlow.Rows[0]["FLOWCATEGORYTEMPLETE"], dtFlow.Rows[0]["FLOWSUBCATEGORYTEMPLETE"],
                            dtFlow.Rows[0]["TASKNAMETEMPLETE"], dtFlow.Rows[0]["TASKCATEGORYTEMPLETE"],
                            dtFlow.Rows[0]["TASKSUBCATEGORYTEMPLETE"], dtFlow.Rows[0]["INITVARIABLE"],
                            dtFlow.Rows[0]["ALLOWDELETEFORM"], dtFlow.Rows[0]["SENDMSGTOAPPLICANT"],
                            dtFlow.Rows[0]["MODIFYTIME"], dtFlow.Rows[0]["CATEGORYID"], dtFlow.Rows[0]["DESCRIPTION"],
                            dtFlow.Rows[0]["FORMNUMBERPARTA"], dtFlow.Rows[0]["FORMNUMBERPARTB"],
                            dtFlow.Rows[0]["FORMNUMBERPARTC"], dtFlow.Rows[0]["ALREADYRELEASED"]), "clob",
                        dtFlow.Rows[0]["VIEWCONFIG"].ToString());

                    LogWriter.Info(item.Code + "已添加流程主体。");

                    //添加环节
                    const string sqlStep = "SELECT * FROM S_WF_DEFSTEP WHERE DEFFLOWID='{0}'";
                    var dtStep = sqlHelper2.ExecuteDataTable(String.Format(sqlStep, item.ID));
                    if (dtStep.Rows.Count > 0)
                    {
                        const string sqlInserStep =
                            "INSERT INTO S_WF_DEFSTEP(" +
                            "ID, " +
                            "DEFFLOWID," +
                            "CODE," +
                            "NAME," +
                            "TYPE," +
                            "SORTINDEX," +
                            "ALLOWDELEGATE," +
                            "ALLOWCIRCULATE," +
                            "ALLOWASK," +
                            "ALLOWSAVE," +
                            "SAVEVARIABLEAUTH," +
                            "SUBFLOWCODE," +
                            "WAITINGSTEPIDS," +
                            "COOPERATIONMODE," +
                            "PHASE," +
                            "HIDDENELEMENTS," +
                            "VISIBLEELEMENTS," +
                            "EDITABLEELEMENTS," +
                            "DISABLEELEMENTS," +
                            "URGENCY" +
                            ") VALUES('{0}'," +
                            "'{1}'," +
                            "'{2}'," +
                            "'{3}'," +
                            "'{4}'," +
                            "'{5}'," +
                            "'{6}'," +
                            "'{7}'," +
                            "'{8}'," +
                            "'{9}'," +
                            "to_char('{10}')," +
                            "'{11}'," +
                            "'{12}'," +
                            "'{13}'," +
                            "'{14}'," +
                            "'{15}'," +
                            "'{16}'," +
                            "'{17}'," +
                            "'{18}'," +
                            "'{19}')";
                        for (int j = 0; j < dtStep.Rows.Count; j++)
                        {
                            sqlHelper1.ExecuteNonQuery(String.Format(sqlInserStep, dtStep.Rows[j]["ID"],
                                dtStep.Rows[j]["DEFFLOWID"], dtStep.Rows[j]["CODE"],
                                dtStep.Rows[j]["NAME"], dtStep.Rows[j]["TYPE"], dtStep.Rows[j]["SORTINDEX"],
                                dtStep.Rows[j]["ALLOWDELEGATE"], dtStep.Rows[j]["ALLOWCIRCULATE"],
                                dtStep.Rows[j]["ALLOWASK"], dtStep.Rows[j]["ALLOWSAVE"],
                                dtStep.Rows[j]["SAVEVARIABLEAUTH"], dtStep.Rows[j]["SUBFLOWCODE"],
                                dtStep.Rows[j]["WAITINGSTEPIDS"], dtStep.Rows[j]["COOPERATIONMODE"],
                                dtStep.Rows[j]["PHASE"], dtStep.Rows[j]["HIDDENELEMENTS"],
                                dtStep.Rows[j]["VISIBLEELEMENTS"], dtStep.Rows[j]["EDITABLEELEMENTS"],
                                dtStep.Rows[j]["DISABLEELEMENTS"], dtStep.Rows[j]["URGENCY"]));
                        }

                        LogWriter.Info(item.Code + "已添加流程环节。");
                    }

                    //添加路由
                    const string sqlRouting = "SELECT * FROM S_WF_DEFROUTING WHERE DEFFLOWID='{0}'";
                    var dtRouting = sqlHelper2.ExecuteDataTable(String.Format(sqlRouting, item.ID));
                    if (dtRouting.Rows.Count > 0)
                    {
                        const string sqlInserRouting = "INSERT INTO S_WF_DefRouting(" +
                                                       "ID, " +
                                                       "DEFFLOWID, " +
                                                       "DEFSTEPID," +
                                                       "ENDID," +
                                                       "CODE," +
                                                       "NAME," +
                                                       "TYPE," +
                                                       "VALUE," +
                                                       "NOTNULLFIELDS," +
                                                       "VARIABLESET," +
                                                       "FORMDATASET," +
                                                       "SAVEFORM," +
                                                       "MUSTINPUTCOMMENT," +
                                                       "SAVEFORMVERSION," +
                                                       "DEFAULTCOMMENT," +
                                                       "SIGNATUREFIELD," +
                                                       "SIGNATUREPROTECTFIELDS," +
                                                       "SIGNATUREDIVID," +
                                                       "SIGNATURECANCELDIVIDS," +
                                                       "SORTINDEX," +
                                                       "AUTHFORMDATA," +
                                                       "AUTHORGIDS," +
                                                       "AUTHORGNAMES," +
                                                       "AUTHROLEIDS," +
                                                       "AUTHROLENAMES," +
                                                       "AUTHUSERIDS," +
                                                       "AUTHUSERNAMES," +
                                                       "AUTHVARIABLE," +
                                                       "USERIDS," +
                                                       "USERNAMES," +
                                                       "USERIDSFROMSTEP," +
                                                       "USERIDSFROMSTEPSENDER," +
                                                       "USERIDSFROMSTEPEXEC," +
                                                       "USERIDSFROMFIELD," +
                                                       "USERIDSGROUPFROMFIELD," +
                                                       "ROLEIDS," +
                                                       "ROLENAMES," +
                                                       "ROLEIDSFROMFIELD," +
                                                       "ORGIDS," +
                                                       "ORGNAMES," +
                                                       "ORGIDFROMFIELD," +
                                                       "ORGIDFROMUSER," +
                                                       "SELECTMODE," +
                                                       "SELECTAGAIN," +
                                                       "ALLOWWITHDRAW) VALUES(" +
                                                       "'{0}'," +
                                                       "'{1}'," +
                                                       "'{2}'," +
                                                       "'{3}'," +
                                                       "'{4}'," +
                                                       "'{5}'," +
                                                       "'{6}'," +
                                                       "'{7}'," +
                                                       "'{8}'," +
                                                       "to_char('{9}')," +
                                                       "to_char('{10}')," +
                                                       "'{11}'," +
                                                       "'{12}'," +
                                                       "'{13}'," +
                                                       "'{14}'," +
                                                       "'{15}'," +
                                                       "'{16}'," +
                                                       "'{17}'," +
                                                       "'{18}'," +
                                                       "'{19}'," +
                                                       "to_char('{20}')," +
                                                       "'{21}'," +
                                                       "'{22}'," +
                                                       "'{23}'," +
                                                       "'{24}'," +
                                                       "'{25}'," +
                                                       "'{26}'," +
                                                       "to_char('{27}')," +
                                                       "'{28}'," +
                                                       "'{29}'," +
                                                       "'{30}'," +
                                                       "'{31}'," +
                                                       "'{32}'," +
                                                       "'{33}'," +
                                                       "'{34}'," +
                                                       "'{35}'," +
                                                       "'{36}'," +
                                                       "'{37}'," +
                                                       "'{38}'," +
                                                       "'{39}'," +
                                                       "'{40}'," +
                                                       "'{41}'," +
                                                       "'{42}'," +
                                                       "'{43}'," +
                                                       "'{44}')";
                        for (int j = 0; j < dtRouting.Rows.Count; j++)
                        {
                            sqlHelper1.ExecuteNonQuery(String.Format(sqlInserRouting, dtRouting.Rows[j]["ID"], dtRouting.Rows[j]["DEFFLOWID"], dtRouting.Rows[j]["DEFSTEPID"], dtRouting.Rows[j]["ENDID"], dtRouting.Rows[j]["CODE"], dtRouting.Rows[j]["NAME"], dtRouting.Rows[j]["TYPE"], dtRouting.Rows[j]["VALUE"], dtRouting.Rows[j]["NOTNULLFIELDS"], resetValue(dtRouting.Rows[j]["VARIABLESET"]), resetValue(dtRouting.Rows[j]["FORMDATASET"]), dtRouting.Rows[j]["SAVEFORM"], dtRouting.Rows[j]["MUSTINPUTCOMMENT"], dtRouting.Rows[j]["SAVEFORMVERSION"], dtRouting.Rows[j]["DEFAULTCOMMENT"], dtRouting.Rows[j]["SIGNATUREFIELD"], dtRouting.Rows[j]["SIGNATUREPROTECTFIELDS"], dtRouting.Rows[j]["SIGNATUREDIVID"], dtRouting.Rows[j]["SIGNATURECANCELDIVIDS"], dtRouting.Rows[j]["SORTINDEX"], resetValue(dtRouting.Rows[j]["AUTHFORMDATA"]), dtRouting.Rows[j]["AUTHORGIDS"], dtRouting.Rows[j]["AUTHORGNAMES"], dtRouting.Rows[j]["AUTHROLEIDS"], dtRouting.Rows[j]["AUTHROLENAMES"], dtRouting.Rows[j]["AUTHUSERIDS"], dtRouting.Rows[j]["AUTHUSERNAMES"], resetValue(dtRouting.Rows[j]["AUTHVARIABLE"]), dtRouting.Rows[j]["USERIDS"], dtRouting.Rows[j]["USERNAMES"], dtRouting.Rows[j]["USERIDSFROMSTEP"], dtRouting.Rows[j]["USERIDSFROMSTEPSENDER"], dtRouting.Rows[j]["USERIDSFROMSTEPEXEC"], dtRouting.Rows[j]["USERIDSFROMFIELD"], dtRouting.Rows[j]["USERIDSGROUPFROMFIELD"], dtRouting.Rows[j]["ROLEIDS"], dtRouting.Rows[j]["ROLENAMES"], dtRouting.Rows[j]["ROLEIDSFROMFIELD"], dtRouting.Rows[j]["ORGIDS"], dtRouting.Rows[j]["ORGNAMES"], dtRouting.Rows[j]["ORGIDFROMFIELD"], dtRouting.Rows[j]["ORGIDFROMUSER"], dtRouting.Rows[j]["SELECTMODE"], dtRouting.Rows[j]["SELECTAGAIN"], dtRouting.Rows[j]["ALLOWWITHDRAW"]));
                        }

                        LogWriter.Info(item.Code + "已添加流程路由。");
                    }
                }

                LogWriter.Info(item.Code + " ===========完成更新==========");
            }
        }

        private string resetValue(object name)
        {
            string str = null;
            if (name != null)
            {
                str = name.ToString();
                if (str.Contains("'"))
                {
                    str = str.Replace("'", "''");
                }
            }
            return str;
        }
    }

    class IdAndCode
    {
        public string ID { get; set; }

        public string Code { get; set; }
    }
}
