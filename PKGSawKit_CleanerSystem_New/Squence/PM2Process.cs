using MsSqlManagerLibrary;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace PKGSawKit_CleanerSystem_New.Squence
{
    struct TPrcsRecipe2
    {
        public int TotalStep;           // Process total step
        public int StepNum;             // Process step number

        public string[] StepName;       // Process step name        
        public string[] Air1;
        public string[] Air2;
        public string[] Water1;        
        public string[] Water2;
        public string[] Water3;
        public string[] Water4;
        public string[] Water5;
        public double[] ProcessTime;     // Process time
    }

    class PM2Process : TBaseThread
    {
        Thread thread;
        private new TStep step;
        TPrcsRecipe2 prcsRecipe; // Recipe struct
        Alarm_List alarm_List;  // Alarm list        

        public PM2Process()
        {
            ModuleName = "PM2";
            module = (byte)MODULE._PM2;

            thread = new Thread(new ThreadStart(Execute));

            prcsRecipe = new TPrcsRecipe2();            
            alarm_List = new Alarm_List();

            prcsRecipe.StepName = new string[Define.RECIPE_MAX_STEP];       // Max 50 step
            prcsRecipe.Air1 = new string[Define.RECIPE_MAX_STEP];
            prcsRecipe.Air2 = new string[Define.RECIPE_MAX_STEP];
            prcsRecipe.Water1 = new string[Define.RECIPE_MAX_STEP];            
            prcsRecipe.Water2 = new string[Define.RECIPE_MAX_STEP];
            prcsRecipe.Water3 = new string[Define.RECIPE_MAX_STEP];
            prcsRecipe.Water4 = new string[Define.RECIPE_MAX_STEP];
            prcsRecipe.Water5 = new string[Define.RECIPE_MAX_STEP];
            prcsRecipe.ProcessTime = new double[Define.RECIPE_MAX_STEP];

            thread.Start();
        }

        public void Dispose()
        {
            thread.Abort();
        }

        private void Execute()
        {
            try
            {
                while (true)
                {
                    if (Define.seqCtrl[module] == Define.CTRL_ABORT)
                    {
                        AlarmAction("Abort");
                    }
                    else if (Define.seqCtrl[module] == Define.CTRL_RETRY)
                    {
                        AlarmAction("Retry");
                    }

                    Process_Progress();
                    Init_Progress();

                    Thread.Sleep(10);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void AlarmAction(string sAction)
        {
            if (sAction == "Retry")
            {
                if (Define.seqSts[module] == Define.STS_PROCESS_ING)
                {
                    // 자재 공정중인 색상(Lime색?)으로 변경
                }

                step.Flag = true;
                step.Times = 1;

                if ((step.Layer >= 4) && (step.Layer <= 7))
                {
                    step.Layer = 4;
                }

                Define.seqCtrl[module] = Define.CTRL_RUNNING;

                Define.seqCylinderMode[module] = Define.MODE_CYLINDER_RUN;
                Define.seqCylinderCtrl[module] = Define.CTRL_RUN;

                Global.EventLog("Resume process current phase : " + sAction, ModuleName, "Event");
            }
            else if (sAction == "Ignore")
            {
                F_INC_STEP();

                Define.seqCtrl[module] = Define.CTRL_RUNNING;                

                Global.EventLog("Skip the process current step : " + sAction, ModuleName, "Event");
            }
            else if (sAction == "Abort")
            {
                ActionList();

                Define.seqMode[module] = Define.MODE_IDLE;
                Define.seqCtrl[module] = Define.CTRL_IDLE;
                Define.seqSts[module] = Define.STS_ABORTOK;

                step.Times = 1;
                Global.prcsInfo.prcsStepCurrentTime[module] = 1;

                Global.EventLog("Process has stopped : " + sAction, ModuleName, "Event");
            }
        }

        private void ActionList()
        {
            F_PROCESS_ALL_VALVE_CLOSE();

            Define.seqCylinderCtrl[module] = Define.CTRL_ABORT;
        }

        private void ShowAlarm(string almId)
        {
            ActionList();

            Define.seqCtrl[module] = Define.CTRL_ALARM;

            // Buzzer IO On.
            Global.SetDigValue((int)DigOutputList.Buzzer_o, (uint)DigitalOffOn.On, ModuleName);

            // Alarm history.
            Define.sAlarmName = "";
            alarm_List.alarm_code = almId;
            Define.sAlarmName = alarm_List.alarm_code;

            Global.EventLog(almId + ":" + Define.sAlarmName, ModuleName, "Alarm");

            HostConnection.Host_Set_RunStatus(Global.hostEquipmentInfo, ModuleName, "Alarm");
            HostConnection.Host_Set_AlarmName(Global.hostEquipmentInfo, ModuleName, Define.sAlarmName);
        }

        public void F_INC_STEP()
        {
            step.Flag = true;
            step.Layer++;
            step.Times = 1;
        }

        // PROCESS PROGRESS /////////////////////////////////////////////////////////////////
        #region PROCESS_PROGRESS
        private void Process_Progress()
        {
            if ((Define.seqMode[module] == Define.MODE_PROCESS) && (Define.seqCtrl[module] == Define.CTRL_RUN))
            {
                Thread.Sleep(500);
                step.Layer = 1;
                step.Times = 1;
                step.Flag = true;

                prcsRecipe.TotalStep = 0;
                prcsRecipe.StepNum = 0;

                for (int i = 0; i < Define.RECIPE_MAX_STEP; i++)
                {
                    prcsRecipe.StepName[i] = string.Empty;
                    prcsRecipe.Air1[i] = string.Empty;
                    prcsRecipe.Air2[i] = string.Empty;
                    prcsRecipe.Water1[i] = string.Empty;                    
                    prcsRecipe.Water2[i] = string.Empty;
                    prcsRecipe.Water3[i] = string.Empty;
                    prcsRecipe.Water4[i] = string.Empty;
                    prcsRecipe.Water5[i] = string.Empty;
                    prcsRecipe.ProcessTime[i] = 0;
                }

                Global.prcsInfo.prcsRecipeName[module] = string.Empty;
                Global.prcsInfo.prcsCurrentStep[module] = 0;
                Global.prcsInfo.prcsTotalStep[module] = 0;
                Global.prcsInfo.prcsStepName[module] = string.Empty;
                Global.prcsInfo.prcsStepCurrentTime[module] = 1;
                Global.prcsInfo.prcsStepTotalTime[module] = 0;
                Global.prcsInfo.prcsEndTime[module] = string.Empty;
                

                Define.seqCtrl[module] = Define.CTRL_RUNNING;
                Define.seqSts[module] = Define.STS_PROCESS_ING;

                Global.EventLog("START THE PROCESS.", ModuleName, "Event");

                HostConnection.Host_Set_ProcessEndTime(Global.hostEquipmentInfo, ModuleName, "");
                HostConnection.Host_Set_RunStatus(Global.hostEquipmentInfo, ModuleName, "Process");
            }
            else if ((Define.seqMode[module] == Define.MODE_PROCESS) && (Define.seqCtrl[module] == Define.CTRL_RUNNING))
            {                
                switch (step.Layer)
                {
                    case 1:
                        {
                            P_PROCESS_DoorCheck("Close");
                        }
                        break;

                    case 2:
                        {
                            F_INC_STEP();
                        }
                        break;

                    case 3:
                        {
                            P_PROCESS_RecipeLoading(string.Format("{0}{1}\\{2}", Global.RecipeFilePath, ModuleName, Define.sSelectRecipeName[module]));
                        }
                        break;

                    case 4:
                        {
                            F_INC_STEP();
                        }
                        break;

                    case 5:
                        {
                            P_Cylinder_FwdBwd_Seq("Home");
                        }
                        break;

                    case 6:
                        {
                            P_Cylinder_FwdBwd_Seq("Run");
                            
                        }
                        break;

                    case 7:
                        {
                            P_PROCESS_IO_Setting();
                        }
                        break;

                    case 8:
                        {
                            P_PROCESS_ProcessTimeCheck();
                        }
                        break;

                    case 9:
                        {
                            P_PROCESS_EndStepCheck(4);
                        }
                        break;

                    case 10:
                        {                            
                            P_Cylinder_FwdBwd_Seq("Home");
                        }
                        break;

                    case 11:
                        {
                            F_INC_STEP();
                        }
                        break;

                    case 12:
                        {
                            F_INC_STEP();
                        }
                        break;

                    case 13:
                        {
                            P_PROCESS_DoorCheck("Open");
                        }
                        break;

                    case 14:
                        {
                            P_PROCESS_ProcessEnd();
                        }
                        break;
                }
            }
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////

        // INIT PROGRESS ////////////////////////////////////////////////////////////////////
        #region INIT_PROGRESS
        private void Init_Progress()
        {
            if ((Define.seqMode[module] == Define.MODE_INIT) && (Define.seqCtrl[module] == Define.CTRL_RUN))
            {
                Thread.Sleep(500);
                step.Layer = 1;
                step.Times = 1;
                step.Flag = true;

                Define.seqCtrl[module] = Define.CTRL_RUNNING;
                Define.seqSts[module] = Define.STS_INIT_ING;

                Global.EventLog("START THE INITIALIZE.", ModuleName, "Event");

                HostConnection.Host_Set_RunStatus(Global.hostEquipmentInfo, ModuleName, "Init");
            }
            else if ((Define.seqMode[module] == Define.MODE_INIT) && (Define.seqCtrl[module] == Define.CTRL_RUNNING))
            {
                switch (step.Layer)
                {
                    case 1:
                        {
                            P_INIT_ALLVALVECLOSE();
                        }
                        break;

                    case 2:
                        {
                            P_PROCESS_DoorCheck("Close");
                        }
                        break;

                    case 3:
                        {
                            F_INC_STEP();
                        }
                        break;

                    case 4:
                        {
                            P_Cylinder_FwdBwd_Seq("Home");
                        }
                        break;

                    case 5:
                        {
                            P_PROCESS_DoorCheck("Open");
                        }
                        break;

                    case 6:
                        {
                            P_INIT_End();
                        }
                        break;
                }
            }
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////
        
        // FUNCTION /////////////////////////////////////////////////////////////////////////
        #region PROCESS FUNCTION        
        private void P_PROCESS_DoorCheck(string OpCl)
        {
            if (step.Flag)
            {
                Global.EventLog("Check the door " + OpCl, ModuleName, "Event");
                
                if (OpCl == "Open")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_Door_Open_o, (uint)DigitalOffOn.On, ModuleName);
                    Global.SetDigValue((int)DigOutputList.CH2_Door_Close_o, (uint)DigitalOffOn.Off, ModuleName);
                }
                else if (OpCl == "Close")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_Door_Open_o, (uint)DigitalOffOn.Off, ModuleName);
                    Global.SetDigValue((int)DigOutputList.CH2_Door_Close_o, (uint)DigitalOffOn.On, ModuleName);
                }
                
                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                if (Define.bInterlockRelease)
                {
                    F_INC_STEP();
                    return;
                }

                if (OpCl == "Open")
                {
                    if ((Global.GetDigValue((int)DigInputList.CH2_Door_Op_i) == "On") &&
                        (Global.GetDigValue((int)DigInputList.CH2_Door_Cl_i) == "Off"))
                    {
                        F_INC_STEP();
                    }
                    else
                    {
                        if (step.Times >= Configure_List.Door_OpCl_Timeout)
                        {
                            ShowAlarm("1000");
                        }
                        else
                        {
                            step.INC_TIMES();
                        }
                    }
                }                    
                else
                {
                    if ((Global.GetDigValue((int)DigInputList.CH2_Door_Op_i) == "Off") &&
                        (Global.GetDigValue((int)DigInputList.CH2_Door_Cl_i) == "On"))
                    {
                        F_INC_STEP();
                    }
                    else
                    {
                        if (step.Times >= Configure_List.Door_OpCl_Timeout)
                        {
                            ShowAlarm("1001");
                        }
                        else
                        {
                            step.INC_TIMES();
                        }
                    }
                }                               
            }
        }

        private void P_PROCESS_RecipeLoading(string FileName)
        {
            if (step.Flag)
            {
                Global.EventLog("Loading the process recipe file.", ModuleName, "Event");

                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                if (File.Exists(FileName))
                {
                    ImportExcelData_Read(FileName);

                    prcsRecipe.StepNum = 1; // Recipe 현재 스탭 초기화

                    Global.EventLog("Recipe file was successfully read.", ModuleName, "Event");

                    Global.prcsInfo.prcsRecipeName[module] = Define.sSelectRecipeName[module];

                    Global.EventLog("Recipe name : " + Global.prcsInfo.prcsRecipeName[module], ModuleName, "Event");

                    HostConnection.Host_Set_RecipeName(Global.hostEquipmentInfo, ModuleName, Global.prcsInfo.prcsRecipeName[module]);

                    F_INC_STEP();
                }
                else
                {
                    ShowAlarm("1010");  // "Failed to read recipe file."
                }
            }
        }

        private void ImportExcelData_Read(string fileName)
        {
            uint lineNum = 0;   // Recipe 파일의 item line 총 갯수

            try
            {
                StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    if (lineNum == 0)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        int iDataCnt = data.Length - 1;
                        prcsRecipe.TotalStep = iDataCnt;    // Process total step count

                        lineNum++;
                    }
                    else if (lineNum == 1)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.StepName[i] = data[i + 1];   // Process step name
                        }

                        lineNum++;
                    }
                    else if (lineNum == 2)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.Air1[i] = data[i + 1];   // Air1
                        }

                        lineNum++;
                    }
                    else if (lineNum == 3)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.Air2[i] = data[i + 1];   // Air2
                        }

                        lineNum++;
                    }
                    else if (lineNum == 4)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.Water1[i] = data[i + 1];   // Water1
                        }

                        lineNum++;
                    }                    
                    else if (lineNum == 5)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.Water2[i] = data[i + 1];   // Water2
                        }

                        lineNum++;
                    }
                    else if (lineNum == 6)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.Water3[i] = data[i + 1];   // Water3
                        }

                        lineNum++;
                    }
                    else if (lineNum == 7)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.Water4[i] = data[i + 1];   // Water4
                        }

                        lineNum++;
                    }
                    else if (lineNum == 8)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.Water5[i] = data[i + 1];   // Water5
                        }

                        lineNum++;
                    }
                    else if (lineNum == 9)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < prcsRecipe.TotalStep; i++)
                        {
                            prcsRecipe.ProcessTime[i] = double.Parse(data[i + 1]);    // Process step time
                        }

                        sr.Close();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림");
            }
        }

        private void P_PROCESS_IO_Setting()
        {
            if (step.Flag)
            {
                Global.prcsInfo.prcsCurrentStep[module] = prcsRecipe.StepNum;
                Global.prcsInfo.prcsTotalStep[module] = prcsRecipe.TotalStep;
                Global.prcsInfo.prcsStepName[module] = prcsRecipe.StepName[prcsRecipe.StepNum - 1];
                Global.prcsInfo.prcsStepCurrentTime[module] = 1;
                Global.prcsInfo.prcsStepTotalTime[module] = prcsRecipe.ProcessTime[prcsRecipe.StepNum - 1];

                Global.EventLog("Process Step : " + (prcsRecipe.StepNum).ToString(), ModuleName, "Event");

                // 서버에 매 초 경과되는 Process time을 보내려고 했으나, delay소지가 있어 경과되는 Step num을 보내는 것으로 수정 /////
                string strProgressTime = string.Format("{0}/{1}",
                        Global.prcsInfo.prcsCurrentStep[module].ToString(), Global.prcsInfo.prcsTotalStep[module].ToString());

                HostConnection.Host_Set_ProgressTime(Global.hostEquipmentInfo, ModuleName, strProgressTime);
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Air1
                if (prcsRecipe.Air1[prcsRecipe.StepNum - 1] == "On")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_AirValve_1_o, (uint)DigitalOffOn.On, ModuleName);
                    Global.SetDigValue((int)DigOutputList.CH2_WaterAirValve_o, (uint)DigitalOffOn.On, ModuleName);
                }
                else
                {
                    Global.SetDigValue((int)DigOutputList.CH2_AirValve_1_o, (uint)DigitalOffOn.Off, ModuleName);
                    Global.SetDigValue((int)DigOutputList.CH2_WaterAirValve_o, (uint)DigitalOffOn.Off, ModuleName);
                }

                // Air2
                if (prcsRecipe.Air2[prcsRecipe.StepNum - 1] == "On")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_AirValve_2_o, (uint)DigitalOffOn.On, ModuleName);
                }
                else
                {
                    Global.SetDigValue((int)DigOutputList.CH2_AirValve_2_o, (uint)DigitalOffOn.Off, ModuleName);
                }

                // Water1
                if (prcsRecipe.Water1[prcsRecipe.StepNum - 1] == "On")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_1_o, (uint)DigitalOffOn.On, ModuleName);                    
                }
                else
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_1_o, (uint)DigitalOffOn.Off, ModuleName);
                }

                // Water2
                if (prcsRecipe.Water2[prcsRecipe.StepNum - 1] == "On")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_2_o, (uint)DigitalOffOn.On, ModuleName);
                }
                else
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_2_o, (uint)DigitalOffOn.Off, ModuleName);
                }

                // Water3
                if (prcsRecipe.Water3[prcsRecipe.StepNum - 1] == "On")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_3_o, (uint)DigitalOffOn.On, ModuleName);
                }
                else
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_3_o, (uint)DigitalOffOn.Off, ModuleName);
                }

                // Water4
                if (prcsRecipe.Water4[prcsRecipe.StepNum - 1] == "On")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_4_o, (uint)DigitalOffOn.On, ModuleName);
                }
                else
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_4_o, (uint)DigitalOffOn.Off, ModuleName);
                }

                // Water5
                if (prcsRecipe.Water5[prcsRecipe.StepNum - 1] == "On")
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_5_o, (uint)DigitalOffOn.On, ModuleName);
                }
                else
                {
                    Global.SetDigValue((int)DigOutputList.CH2_WaterValve_5_o, (uint)DigitalOffOn.Off, ModuleName);
                }

                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                F_INC_STEP();
            }
        }

        private void P_Cylinder_FwdBwd_Seq(string sAct)
        {
            if (step.Flag)
            {
                if (sAct == "Run")
                {
                    if ((Define.seqCylinderMode[module] == Define.MODE_CYLINDER_IDLE) && (Define.seqCylinderCtrl[module] == Define.CTRL_IDLE))
                    {
                        Define.seqCylinderMode[module] = Define.MODE_CYLINDER_RUN;
                        Define.seqCylinderCtrl[module] = Define.CTRL_RUN;
                        Define.seqCylinderSts[module] = Define.STS_CYLINDER_IDLE;
                    }
                }
                else
                {
                    Define.seqCylinderMode[module] = Define.MODE_CYLINDER_HOME;
                    Define.seqCylinderCtrl[module] = Define.CTRL_RUN;
                    Define.seqCylinderSts[module] = Define.STS_CYLINDER_IDLE;                    
                }

                step.Flag = false;
                step.Times = 1;                
            }
            else
            {
                if (sAct == "Run")
                {
                    F_INC_STEP();
                }
                else
                {
                    if (step.Times > 1)
                    {
                        if ((Define.seqCylinderCtrl[module] == Define.CTRL_IDLE) &&
                            (Define.seqCylinderSts[module] == Define.STS_CYLINDER_HOMEEND))
                        {
                            F_INC_STEP();
                        }
                        else
                        {
                            step.INC_TIMES();
                        }
                    }
                    else
                    {
                        step.INC_TIMES();
                    }
                }
            }
        }

        private void P_PROCESS_ProcessTimeCheck()
        {
            if (step.Flag)
            {
                Global.EventLog("Check the process time : " + prcsRecipe.ProcessTime[prcsRecipe.StepNum - 1].ToString() + " sec.", ModuleName, "Event");

                step.Flag = false;
                step.Times = 1;                
            }
            else
            {
                if (step.Times >= prcsRecipe.ProcessTime[prcsRecipe.StepNum - 1])
                {                    
                    F_INC_STEP();
                }
                else
                {
                    step.INC_TIMES();
                    
                    // Ui에 표시 할 시간
                    Global.prcsInfo.prcsStepCurrentTime[module] = step.Times;
                }
            }
        }

        private void P_PROCESS_EndStepCheck(byte nStep)
        {
            if (step.Flag)
            {
                Global.EventLog("Check the End step.", ModuleName, "Event");

                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                if (prcsRecipe.StepNum >= prcsRecipe.TotalStep)
                {
                    F_PROCESS_ALL_VALVE_CLOSE();                                                 

                    F_INC_STEP();                   
                }
                else
                {
                    prcsRecipe.StepNum++;                    

                    Global.prcsInfo.prcsStepCurrentTime[module] = 1;

                    step.Flag = true;
                    step.Layer = nStep;                    
                }
            }
        }

        private void P_PROCESS_ProcessEnd()
        {
            Global.prcsInfo.prcsEndTime[module] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            HostConnection.Host_Set_ProcessEndTime(Global.hostEquipmentInfo, ModuleName, Global.prcsInfo.prcsEndTime[module]);

            Define.seqMode[module] = Define.MODE_IDLE;
            Define.seqCtrl[module] = Define.CTRL_IDLE;
            Define.seqSts[module] = Define.STS_PROCESS_END;

            // Process end buzzer
            //Global.SetDigValue((int)DigOutputList.Buzzer_o, (uint)DigitalOffOn.On, ModuleName);

            Global.EventLog("PROCESS COMPLETED.", ModuleName, "Event");

            F_DAILY_COUNT();
        }        

        private void F_PROCESS_ALL_VALVE_CLOSE()
        {
            // Air
            Global.SetDigValue((int)DigOutputList.CH2_AirValve_1_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_AirValve_2_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterAirValve_o, (uint)DigitalOffOn.Off, ModuleName);

            // Water
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_1_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_2_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_3_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_4_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_5_o, (uint)DigitalOffOn.Off, ModuleName);
        }
        
        private void F_DAILY_COUNT()
        {
            Define.iPM2DailyCnt++;
            Global.DailyLog(Define.iPM2DailyCnt, ModuleName);            
        }
        #endregion

        #region INIT FUNCTION
        private void P_INIT_ALLVALVECLOSE()
        {
            if (step.Flag)
            {
                F_PROCESS_ALL_VALVE_CLOSE();

                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                F_INC_STEP();
            }
        }        

        private void P_INIT_End()
        {
            Define.seqMode[module] = Define.MODE_IDLE;
            Define.seqCtrl[module] = Define.CTRL_IDLE;
            Define.seqSts[module] = Define.STS_INIT_END;

            Global.EventLog("INIT COMPLETED.", ModuleName, "Event");            
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////
    }
}
