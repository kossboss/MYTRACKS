using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using MyTracksData.Properties;
using ZedGraph;

namespace MyTracksData
{
    public partial class Form1 : Form
    {
        PointPairList listSandT = new PointPairList();
        PointPairList listSmax_andT = new PointPairList();
        PointPairList listSmin_andT = new PointPairList();
        PointPairList listSavg_andT = new PointPairList();
        PointPairList listDandT = new PointPairList();
        PointPairList listAandT = new PointPairList();
        PointPairList listAmax_andT = new PointPairList();
        PointPairList listAmin_andT = new PointPairList();
        PointPairList listAavg_andT = new PointPairList();
        PointPairList listLat_Long = new PointPairList();
        //PointPairList listSandT = new PointPairList();

        List<classDataPoint> Points = new List<classDataPoint>();
        DataTable tbl = new DataTable();
        DataTable tblf = new DataTable();
        string filename1 = "";
        public Form1()
        {
            InitializeComponent();
            inittable();
            disableGmenu();

        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main();
        }
        private void inittable() {
            //Data Point # (2958 Useful/2959 Total) = 08:57:59 08/28/2012, Lat: 37.2758320*, Long: -121.8517480*, Alt: 15.175 m = 49.785 ft, Spd: 5.730 m/s = 12.817 mph, Br: 235.577* --- ACC: 6 -- Delta_D: 26.204 ft, Total Distance: 30.801 mi
            //       INFO - min_MPH: 0.000, max_MPH: 72.871, avgMPH_data: 35.121, avgMPH_map_real: 15.015, avgMPH_map_travel: 28.968, min_ALT: -220.756, max_ALT: 123.701, avg_ALT: -46.633, dTime_Travel(sec): 1.969, Travel(hr): 1.063, Real(hr): 2.051, Rest(hr): 0.988

            tbl.Columns.Add("i", typeof(int));     //1
            tbl.Columns.Add("Date & Time", typeof(DateTime)); //2
            tbl.Columns.Add("Real Time [Hours]", typeof(double)); //real 3
            tbl.Columns.Add("Lat [Degrees]", typeof(double)); //4
            tbl.Columns.Add("Long [Degrees]", typeof(double));  //5
            tbl.Columns.Add("Altitude [meters]", typeof(double));  //6
            tbl.Columns.Add("Altitude [ft]", typeof(double));  //6.5
            tbl.Columns.Add("Speed [m/s]", typeof(double)); //7
            tbl.Columns.Add("Speed [mph]", typeof(double));  //8
            tbl.Columns.Add("Bearing [Degrees]", typeof(double));  //9
            tbl.Columns.Add("Accuracy", typeof(double)); //10
            tbl.Columns.Add("Distance from last point [ft]", typeof(double)); //11
            tbl.Columns.Add("Total Distance [miles]", typeof(double));  //12
            tbl.Columns.Add("Min Speed [mph]", typeof(double));  //13
            tbl.Columns.Add("Max Speed [mph]", typeof(double));  //14
            tbl.Columns.Add("Average Speed from Data [mph]", typeof(double));  //15
            tbl.Columns.Add("Average Speed based on Real Time [mph]", typeof(double)); //16
            tbl.Columns.Add("Average Speed based on Travel Time [mph]", typeof(double));  //17
            tbl.Columns.Add("Min Altitude [ft]", typeof(double));  //18
            tbl.Columns.Add("Max Altitude [ft]", typeof(double));  //19
            tbl.Columns.Add("Average Altitude [ft]", typeof(double));  //20
            tbl.Columns.Add("Travel Time [Hours]", typeof(double));   //21
            tbl.Columns.Add("Time at rest/stopped [Hours]", typeof(double));  //22
            //

            tblf.Columns.Add("i", typeof(int));     //1
            tblf.Columns.Add("Date & Time", typeof(DateTime)); //2
            tblf.Columns.Add("Real Time [Hours]", typeof(string)); //real 3
            tblf.Columns.Add("Lat [Degrees]", typeof(double)); //4
            tblf.Columns.Add("Long [Degrees]", typeof(double));  //5
            tblf.Columns.Add("Altitude [meters]", typeof(string));  //6
            tblf.Columns.Add("Altitude [ft]", typeof(string));  //6.5
            tblf.Columns.Add("Speed [m/s]", typeof(double)); //7
            tblf.Columns.Add("Speed [mph]", typeof(string));  //8
            tblf.Columns.Add("Bearing [Degrees]", typeof(string));  //9
            tblf.Columns.Add("Accuracy", typeof(double)); //10
            tblf.Columns.Add("Distance from last point [ft]", typeof(string)); //11
            tblf.Columns.Add("Total Distance [miles]", typeof(string));  //12
            tblf.Columns.Add("Min Speed [mph]", typeof(string));  //13
            tblf.Columns.Add("Max Speed [mph]", typeof(string));  //14
            tblf.Columns.Add("Average Speed from Data [mph]", typeof(string));  //15
            tblf.Columns.Add("Average Speed based on Real Time [mph]", typeof(string)); //16
            tblf.Columns.Add("Average Speed based on Travel Time [mph]", typeof(string));  //17
            tblf.Columns.Add("Min Altitude [ft]", typeof(string));  //18
            tblf.Columns.Add("Max Altitude [ft]", typeof(string));  //19
            tblf.Columns.Add("Average Altitude [ft]", typeof(string));  //20
            tblf.Columns.Add("Travel Time [Hours]", typeof(string));   //21
            tblf.Columns.Add("Time at rest/stopped [Hours]", typeof(string));  //22

          

        }



        private void showtable()
        {
           
            table tb = new table();
            tb.Show();
            tb.Text = "Table - Not Formatted";
            tb.dg.DataSource = tbl;
         //   tb.dg.
         //   tb.dg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
       //     tb.dg.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);  
         }

        private void showtablef()
        {

            table tb = new table();
            tb.Show();
            tb.Text = "Table - Formatted";
            tb.dg.DataSource = tblf;
            //   tb.dg.
          //  tb.dg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //     tb.dg.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);  
        }



        private void saveXML(DataTable table_to_save, bool isthisformatted)
        {
           
            FileInfo probe = new FileInfo(filename1);
            string filename_xmlout = "";
            string extra = ": NOT Formatted";
            string extra1 = "(NOT FORMATTED)";
            double factor = 21.8778; //got factors from simple calc
            if (isthisformatted) 
            {
                extra1 = "(FORMATTED)";
                extra = ": Formatted";
                factor=20.647;
            }
            double sizeguess=0;
            double actualsize=0;
            sizeguess = (((Convert.ToDouble(probe.Length) * factor) / 1024) / 1024);
            SaveFileDialog fsave = new SaveFileDialog();
            fsave.Title = String.Format("Output XML File"+ extra + " - Approx File Size: ~{0:0.0} MB", sizeguess)  ;
            fsave.DefaultExt = ".xml";
            fsave.Filter = "XML (.xml)|*.xml|All Files (*.*)|*.*";
            fsave.FilterIndex = 1;
//            fsave.Filter = fsave.DefaultExt;
           
            if (fsave.ShowDialog() == DialogResult.OK)
            {

                filename_xmlout = fsave.FileName;

                if (filename_xmlout.ToLower().Equals(filename1.ToLower())) 
                {
                    rtb.AppendText("* Error Saving XML: Cant overwrite Original CSV File" + Environment.NewLine); rtb.Refresh();
                    return;
                }
                if (filename_xmlout.Equals(""))
                {
                    rtb.AppendText("* Error Saving XML: EMPTY FILE" + Environment.NewLine); rtb.Refresh();
                  return;
                }


                DataSet set = new DataSet(filename1 + " MoreTracks by Kossboss");
                set.Tables.Add(table_to_save);
                set.WriteXml(filename_xmlout, XmlWriteMode.DiffGram);
                probe = new FileInfo(filename_xmlout);
                actualsize = (Convert.ToDouble(probe.Length)/1024)/1024;
                rtb.AppendText( String.Format("* Saved Table to XML " +extra1+" - Size: {0:0.000} MB, File Name: {1}", actualsize, filename_xmlout)+ Environment.NewLine); rtb.Refresh();
            }
            else
            {
                return;
            }




        }


        private void Main()
        {
            
            OpenFileDialog fopen = new OpenFileDialog();
            fopen.Multiselect = false;
            fopen.Filter = "CSV (.csv)|*.csv|All Files (*.*)|*.*";
            fopen.FilterIndex = 1;
            if (fopen.ShowDialog() == DialogResult.OK)
            {
              
                ReadFile(fopen.FileName);
               
            }
            else
            {
                return;
            }
        }
        private void ReadFile(string filename)
        {
            filename1 = filename;
            disableGmenu();
            rtb.AppendText("#######################" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("* Started Read File: " + filename + Environment.NewLine); rtb.Refresh();
            try
            {
                FileStream fileStreamTEST = new FileStream(filename, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: File is probably in use." + Environment.NewLine + Environment.NewLine + "Details:" + Environment.NewLine + e.ToString());//+ Environment.NewLine + " Or: It could be another error of file loading: " + Environment.NewLine + e.ToString());
                rtb.AppendText("* ERROR: File is probably in use." + Environment.NewLine); rtb.Refresh();
                return;
            }
            rtb.AppendText("* File Checked Good" + Environment.NewLine); rtb.Refresh();
            int _bufferSize = 16384;
            StringBuilder stringBuilder1 = new StringBuilder();
            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                rtb.AppendText("* Opening File" + Environment.NewLine); rtb.Refresh();
                char[] fileContents = new char[_bufferSize];
                int charsRead = streamReader.Read(fileContents, 0, _bufferSize);

                if (charsRead == 0)
                {
                    rtb.AppendText("* ERROR: Error Opening file its 0 Bytes. Its an Empty File" + Environment.NewLine); rtb.Refresh();
                    throw new Exception("File is 0 bytes");
                }

                while (charsRead > 0)
                {
                    stringBuilder1.Append(fileContents);
                    charsRead = streamReader.Read(fileContents, 0, _bufferSize);
                }
                rtb.AppendText("* Done opening file" + Environment.NewLine); rtb.Refresh();
            }
            //"Segment","Point","Latitude (deg)","Longitude (deg)","Altitude (m)",
            //"Bearing (deg)","Accuracy (m)","Speed (m/s)","Time","Power (W)","Cadence (rpm)","Heart rate (bpm)","Battery level (%)"
            rtb.AppendText("* File Parsed" + Environment.NewLine);
            rtb.AppendText("* Started building the string" + Environment.NewLine); rtb.Refresh();
            string wholestring = stringBuilder1.ToString();
            rtb.AppendText("* Built the string: COMPLETE" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("* Checking if Valid file Started" + Environment.NewLine); rtb.Refresh();
            string valtest;
            valtest = Resources.ValidityTest;
            if (wholestring.IndexOf(valtest) == -1)
            {
                rtb.AppendText("* ERROR: FAILED - NOT A MYTRACKS FILE" + Environment.NewLine); rtb.Refresh();
                return;
            }
            rtb.AppendText("* Legit MyTracks CSV File Started" + Environment.NewLine); rtb.Refresh();
            int sp1 = wholestring.IndexOf("(%)") + 5; // startpoint1

            if (sp1 == -1)
            {
                MessageBox.Show("ERROR: Probably Wrong File Type, Choose MyTracks CSV files only");
                rtb.AppendText("* ERROR: Probably Wrong File Type, Choose MyTracks CSV files only" + Environment.NewLine); rtb.Refresh();
                return;
            }
            int ep1 = wholestring.Length;
            string rawdata = wholestring.Substring(sp1);
            rtb.AppendText("* Good File and Raw Data Extracted" + Environment.NewLine); rtb.Refresh();
            char quotec = Convert.ToChar(34);
            string quotes = quotec.ToString();
            string nulls = "";
            string[] split1 = rawdata.Split(Convert.ToChar(10));
            int splitlen = split1.Length;
            // int lengthtogo = 10; //splitlen
            string[] vals;
            string slat, slong, sspeed, salt, stime, sbear, sacc;
            string na = "NA";
            slat = na;
            slong = na;
            sspeed = na;
            salt = na;
            stime = na;
            sbear = na;
            sacc = na;
            int l1;
            int iter_total = splitlen;
            int iter_good = 0;
            string stringdate = "";
            string stringtime = "";
            string[] stringdateA;
            string[] stringtimeA;
            int second1, minute1, hour1, day1, month1, year1;
            second1 = 0;
            minute1 = 0;
            hour1 = 0;
            day1 = 0;
            month1 = 0;
            year1 = 0;
            Stopwatch sw = Stopwatch.StartNew();
            rtb.AppendText("* Starting to Split Info Out at" + DateTime.UtcNow + Environment.NewLine);
            rtb.AppendText("* PLEASE WAIT:" + Environment.NewLine); rtb.Refresh();
            rtb.Refresh();
            StringBuilder res = new StringBuilder();
            if (split1[0].IndexOf(quotec) == -1)
            {
                MessageBox.Show("ERROR: Probably Wrong File Type, Choose MyTracks CSV files only");
                rtb.AppendText("* ERROR: Probably Wrong File Type, Choose MyTracks CSV files only" + Environment.NewLine); rtb.Refresh();
                return;
            }
            string resstr = "";
            bool flag = false;
            double delta_d = 0;
            double total_d = 0;
            double oldlat, oldlong, oldspeed, oldalt;
            double lat1R, long1R, lat2R, long2R;
            double R = 3958.76; //miles earth radius
            double a, c, dLat, dLong;
            double mph, altft;
            double delta_d_ft = 0;
            oldlat = 0;
            oldlong = 0;
            double maxMPH, minMPH, minALTft, avgMPH_data, avgMPH_map_real, avgMPH_map_travel, sumMPH_data, avgALTft, maxALTft, sumALTft;
            maxMPH = 0;
            sumMPH_data = 0;
            avgMPH_data = 0;
            avgMPH_map_real = 0;
            avgMPH_map_travel = 0;
            avgALTft = 0;
            maxALTft = 0;
            minMPH = 9999999;
            minALTft = 9999999;
            sumALTft = 0;
            TimeSpan total_time_hr = new TimeSpan(0);
            double dTimeLow, dTimeHigh, total_travel_hr;
            double dTimeSec = 0;
            DateTime t1, t2;
            dTimeLow = 0;
            dTimeHigh = 0;
            double rest_hours = 0;
            total_travel_hr = 0;
            t1 = new DateTime();
            double mph_old = 0;
            RichTextBox rtf = new RichTextBox();
            // FOR BOLD BUT WAS SLOW -->  int before = 0; int after = 0; int lengthh = 0; string str_bold = ""; string str_notbold = "";
            double REAL_HOURS = 0;
            for (int i = 0; i < splitlen; i++) // START FOR LOOP I
            {
                lat1R = 0;
                long1R = 0;
                lat2R = 0;
                long2R = 0;
                a = 0;
                c = 0;
                flag = false;
                vals = split1[i].Replace(quotes, nulls).Split(',');
                l1 = vals.Length;
                if (l1 > 9)
                {
                    for (int j = 0; j < l1; j++) // START FOR LOOP J
                    {

                        try
                        {
                            slat = vals[2];
                            slong = vals[3];
                            sspeed = vals[7];
                            salt = vals[4];
                            stime = vals[8].ToLower();
                            stringdate = stime.Substring(0, 10);
                            stringtime = stime.Substring(11, 8);
                            stringdateA = stringdate.Split('-');
                            stringtimeA = stringtime.Split(':');
                            second1 = Convert.ToInt16(stringtimeA[2]);
                            minute1 = Convert.ToInt16(stringtimeA[1]);
                            hour1 = Convert.ToInt16(stringtimeA[0]);
                            day1 = Convert.ToInt16(stringdateA[2]);
                            month1 = Convert.ToInt16(stringdateA[1]);
                            year1 = Convert.ToInt16(stringdateA[0]);
                            // cwl(stime + "  date:" + stringdate + "  time=" + stringtime); //2012-09-24t00:10:23.000z  date:2012-09-24  time=00:10:23
                            // cwl(String.Format("day {0} month {1} year {2} second {3} minute {4} hour {5}", day, month, year, second, minute, hour));
                            sbear = vals[5];
                            sacc = vals[6];
                        }
                        catch (Exception)
                        {
                            //{
                            string errstr = "";
                            errstr = (String.Format("ERROR @ (Actual Pt/Total) ({7}/{8})  = t: {0}, Lat: {1}*, Long: {2}*, Alt: {3} m, Spd: {4} m/s, Brg: {5}* -- ACC: {6}", stime, slat, slong, salt, sspeed, sbear, sacc, iter_good, i + 1) + Environment.NewLine);
                            rtb.AppendText(errstr); rtb.Refresh();
                            flag = true;
                            break;
                        }
                    } // END J LOOP
                    if (flag) { continue; }
                    // cwl(slat);
                    try
                    {
                        mph = Convert.ToDouble(sspeed) * 2.23694;
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                    iter_good += 1;
                    // if () {}
                    //delta_d = 0;
                    if (iter_good > 1)
                    {
                        lat1R = oldlat * Math.PI / 180;
                        long1R = oldlong * Math.PI / 180;
                        lat2R = Convert.ToDouble(slat) * Math.PI / 180;
                        long2R = Convert.ToDouble(slong) * Math.PI / 180;
                        dLat = lat2R - lat1R;
                        dLong = long2R - long1R;
                        a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLong / 2) * Math.Sin(dLong / 2) * Math.Cos(lat1R) * Math.Cos(lat2R);
                        c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                        delta_d = R * c;

                        //  cwl(String.Format("i:{0:0.000000} lat1:{1:0.000000} lat2:{2:0.000000} long1:{3:0.000000} long2:{4:0.000000} dlat:{5:0.000000} dlong:{6:0.000000} a:{7:0.000000} c:{8:0.000000} d:{9:0.000000}", i, lat1R, lat2R, long1R, long2R, dLat, dLong, a, c, R * c));

                        total_d += delta_d;
                    }
                    else
                    {
                        delta_d = 0;
                        total_d += 0;
                    }
                    delta_d_ft = delta_d * 5280;
                    //    cwl(String.Format("la:{0:0.000000} lo:{1:0.000000} laD:{2:0.000000} loD:{3:0.000000}", slat,slong,oldlat,oldlong));
                    oldlat = Convert.ToDouble(slat);
                    oldlong = Convert.ToDouble(slong);
                    oldalt = Convert.ToDouble(salt);
                    oldspeed = Convert.ToDouble(sspeed);
                    altft = oldalt * 3.28084;
                    //PUT ANALYSIS STUFF HERE
                    if (iter_good == 1) { t1 = new DateTime(year1, month1, day1, hour1, minute1, second1, DateTimeKind.Unspecified); }
                    else
                    {
                        // dont just figure out t1 do extra work in the else case
                        dTimeLow = 0;
                        dTimeHigh = 0;
                        if (mph_old != 0) { dTimeLow = delta_d / mph_old; }//miles / speed mph previous                 }
                        if (mph != 0) { dTimeHigh = delta_d / mph; } //miles / speed mph current }
                        // add up the average of the two estimations, one made with the previous speed-low, and one made with the current speed-high
                        total_travel_hr += (dTimeLow + dTimeHigh) / 2;
                    }
                    dTimeSec = ((dTimeLow + dTimeHigh) / 2) * 60 * 60;
                    mph_old = mph;
                    // first t
                    t2 = new DateTime(year1, month1, day1, hour1, minute1, second1, DateTimeKind.Unspecified);
                    total_time_hr = new TimeSpan(t2.Ticks - t1.Ticks);
                    REAL_HOURS = total_time_hr.TotalHours;
                    rest_hours = total_time_hr.TotalHours - total_travel_hr;
                    if (mph > maxMPH) { maxMPH = mph; }
                    if (altft > maxALTft) { maxALTft = altft; }
                    if (mph < minMPH) { minMPH = mph; }
                    if (altft < minALTft) { minALTft = altft; }
                    sumMPH_data += mph;
                    avgMPH_data = sumMPH_data / iter_good;
                    sumALTft += altft;
                    avgALTft = sumALTft / iter_good;
                    avgMPH_map_real = total_d / REAL_HOURS; // take into account stops and stuff
                    avgMPH_map_travel = total_d / total_travel_hr;  // just when moving avg is calc
                    //d=rt  so t=d/r
                    //END OF ANALYSIS STUFF
                    Points.Add(new classDataPoint(iter_good, slat, slong, salt, sspeed, second1, minute1, hour1, day1, month1, year1, sacc, delta_d_ft, total_d));
                    // need to bold the next line
                    resstr = String.Format(" Data Point # ({0} Useful/{1} Total) = {2:00}:{3:00}:{4:00} {5:00}/{6:00}/{7:00}, Lat: {8:0.0000000}*, Long: {9:0.0000000}*, Alt: {10:0,0.000} m = {17:0.000} ft, Spd: {11:0.000} m/s = {16:0.000} mph, Br: {12:0.000}* --- ACC: {13} -- Delta_D: {14:0.000} ft, Total Distance: {15:0.000} mi", iter_good, i + 1, hour1, minute1, second1, month1, day1, year1, Convert.ToDouble(slat), Convert.ToDouble(slong), Convert.ToDouble(salt), Convert.ToDouble(sspeed), Convert.ToDouble(sbear), sacc, delta_d_ft, total_d, mph, altft);
                    resstr += Environment.NewLine;
                    resstr += String.Format("                   INFO - min_MPH: {0:0.000}, max_MPH: {1:0.000}, avgMPH_data: {2:0.000}, avgMPH_map_real: {9:0.000}, avgMPH_map_travel: {10:0.000}, min_ALT: {3:0,0.000}, max_ALT: {4:0,0.000}, avg_ALT: {5:0,0.000}, dTime_Travel(sec): {6:0.000}, Travel(hr): {7:0.000}, Real(hr): {8:0.000}, Rest(hr): {11:0.000}", minMPH, maxMPH, avgMPH_data, minALTft, maxALTft, avgALTft, (dTimeHigh + dTimeLow / 2) * 3600, total_travel_hr, REAL_HOURS, avgMPH_map_real, avgMPH_map_travel, rest_hours);
                    res.Append(resstr + Environment.NewLine);
                    // fill up graph points
                    listSandT.Add(REAL_HOURS , mph );
                    listDandT.Add(REAL_HOURS, total_d);
                    listAandT.Add(REAL_HOURS, altft); 
                    //speed
                    listSmax_andT.Add(REAL_HOURS, maxMPH);
                    listSmin_andT.Add(REAL_HOURS, minMPH);
                    listSavg_andT.Add(REAL_HOURS, avgMPH_map_real); 
                    //altitude
                    listAmax_andT.Add(REAL_HOURS, maxALTft);
                    listAmin_andT.Add(REAL_HOURS, minALTft);
                    listAavg_andT.Add(REAL_HOURS, avgALTft);
                    listLat_Long.Add(Convert.ToDouble(slong), Convert.ToDouble(slat));
                    // fill up data table
                    tbl.Rows.Add(iter_good, t2, REAL_HOURS,Convert.ToDouble(slat),Convert.ToDouble(slong), Convert.ToDouble(salt),altft,Convert.ToDouble(sspeed), mph,Convert.ToDouble(sbear),Convert.ToDouble(sacc),delta_d_ft, total_d, minMPH, maxMPH, avgMPH_data,avgMPH_map_real,avgMPH_map_travel,minALTft,maxALTft,avgALTft,total_travel_hr,rest_hours);
                    tblf.Rows.Add(iter_good, t2, f1(REAL_HOURS), Convert.ToDouble(slat), Convert.ToDouble(slong), f1(Convert.ToDouble(salt)), f1(altft), Convert.ToDouble(sspeed), f1(mph), f1(Convert.ToDouble(sbear)), Convert.ToDouble(sacc), f1(delta_d_ft), f1(total_d), f1(minMPH), f1(maxMPH), f1(avgMPH_data), f1(avgMPH_map_real), f1(avgMPH_map_travel), f1(minALTft),f1( maxALTft), f1(avgALTft), f1(total_travel_hr), f1(rest_hours));
                    
                    //           tbl.Columns.Add("i", typeof(int));     //1

                    //tbl.Columns.Add("Date & Time", typeof(DateTime)); //2
                    //tbl.Columns.Add("Real Time [Hours]", typeof(DateTime)); //real 3
                    //tbl.Columns.Add("Lat [Degrees]", typeof(double)); //4
                    //tbl.Columns.Add("Long [Degrees]", typeof(double));  //5
                    //tbl.Columns.Add("Altitude [meters]", typeof(double));  //6
                    //tbl.Columns.Add("Altitude [ft]", typeof(double));  //6.5
                    //tbl.Columns.Add("Speed [m/s]", typeof(double)); //7
                    //tbl.Columns.Add("Speed [mps]", typeof(double));  //8
                    //tbl.Columns.Add("Bearing [Degrees]", typeof(double));  //9
                    //tbl.Columns.Add("Accuracy", typeof(double)); //10
                    //tbl.Columns.Add("Distance from last point [ft]", typeof(double)); //11
                    //tbl.Columns.Add("Total Distance", typeof(double));  //12
                    //tbl.Columns.Add("Min Speed [mph]", typeof(double));  //13
                    //tbl.Columns.Add("Max Speed [mph]", typeof(double));  //14
                    //tbl.Columns.Add("Average Speed from Data [mph]", typeof(double));  //15
                    //tbl.Columns.Add("Average Speed based on Real Time [mph]", typeof(double)); //16
                    //tbl.Columns.Add("Average Speed based on Travel Time [mph]", typeof(double));  //17
                    //tbl.Columns.Add("Min Altitude [ft]", typeof(double));  //18
                    //tbl.Columns.Add("Max Altitude [ft]", typeof(double));  //19
                    //tbl.Columns.Add("Average Altitude [ft]", typeof(double));  //20
                    //tbl.Columns.Add("Travel Time [Hours]", typeof(double));   //21
                    //tbl.Columns.Add("Time at rest/stopped [Hours]", typeof(double));  //22
                  

                }
            } // END I LOOP
            sw.Stop();
            rtb.AppendText("* FINISHED Analyzing & Formatting Info at " + DateTime.UtcNow + "-- Elapsed Time: " + sw.ElapsedMilliseconds + " ms" + Environment.NewLine); rtb.Refresh();
            sw.Restart();
            rtb.AppendText("* Going To Print Text (might take a moment):" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("* PLEASE WAIT:" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("====================================" + Environment.NewLine); rtb.Refresh();
            // RESULTS GO HERE
            rtb.AppendText(res.ToString());
            enableGmenu();
            //


            //BOLD STUFF UNCOMMENT BUT SLOW - BELOW
            rtb.SelectedRtf = rtf.Rtf;
            //BOLD STUFF UNCOMMENT BUT SLOW - ABOVE

            rtb.AppendText("====================================" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("* FINISHED Printing At " + DateTime.UtcNow + "-- Elapsed Time: " + sw.ElapsedMilliseconds + " ms" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("#######################" + Environment.NewLine); rtb.Refresh();
            sw.Reset();
        }
        public string f1(double inputdouble)
        {
            return String.Format("{0:0.0000}",inputdouble);
            
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { this.Close(); }
        private void Form1_Load(object sender, EventArgs e) { }
        private void cwl(string str) { Console.WriteLine(str); }
        private void cw(string str) { Console.Write(str); }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void disableGmenu() {
            graphToolStripMenuItem.Enabled = false;
            tableToolStripMenuItem.Enabled = false;
            listDandT.Clear();
            listSandT.Clear();
            listAandT.Clear();
            tbl.Rows.Clear();
            tblf.Rows.Clear();
        }
        private void enableGmenu()
        {
            graphToolStripMenuItem.Enabled = true;
            tableToolStripMenuItem.Enabled = true;
        }

        private void speedAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph4stuff(listSandT,listSmax_andT,listSmin_andT,listSavg_andT, "Speed Vs. Total(Real) Time", "Time [Hours]", "Speed [mph]" );
        }
        private void distanceAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphstuff(listDandT, "Distance Vs. Total(Real) Time", "Time [Hours]", "Distance [miles]");
        }

        private void altitudeAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph4stuff(listAandT, listAmax_andT, listAmin_andT, listAavg_andT, "Altitude Vs. Total(Real) Time", "Time [Hours]", "Altitude [ft]");
        }

        public void graphstuff(PointPairList points, string Title, string x_title, string y_title)
        {
            graph g = new graph();
            g.Show();
            GraphPane myPane = g.zgc.GraphPane;
            myPane.Title.Text = Title;
            myPane.XAxis.Title.Text = x_title;
            myPane.YAxis.Title.Text = y_title;
            LineItem myCurve = myPane.AddCurve(y_title, points, Color.Blue, SymbolType.Default);
            // myCurve.Line.Fill = new Fill(Color.White, Color.Red, 45F);
            myCurve.Symbol.Size = 2;
            myCurve.Symbol.Fill = new Fill(Color.Red);
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);
            myPane.YAxis.MajorGrid.IsVisible = true;

            myPane.YAxis.MinorGrid.IsVisible = true;

            myPane.XAxis.MajorGrid.IsVisible = true;

            myPane.XAxis.MinorGrid.IsVisible = true;

            g.zgc.AxisChange();
         }

        public void graph4stuff(PointPairList points0, PointPairList points1_max, PointPairList points2_min, PointPairList points3_avg, string Title, string x_title, string y_title)
        {
            graph g = new graph();
            g.Show();

            GraphPane myPane = g.zgc.GraphPane;
            myPane.Title.Text = Title;
            myPane.XAxis.Title.Text = x_title;
            myPane.YAxis.Title.Text = y_title;
            //p0
            LineItem myCurve0 = myPane.AddCurve(y_title, points0, Color.Black, SymbolType.Default);
            myCurve0.Symbol.Size = 2;
            myCurve0.Symbol.Fill = new Fill(Color.Black);
            //p1-max
            LineItem myCurve1 = myPane.AddCurve(y_title+ " Max", points1_max, Color.Red, SymbolType.Default);
            myCurve1.Symbol.Size = 2;
            myCurve1.Symbol.Fill = new Fill(Color.Red);
            //p2-min
            LineItem myCurve2 = myPane.AddCurve(y_title + " Min", points2_min, Color.Blue, SymbolType.Default);
            myCurve2.Symbol.Size = 2;
            myCurve2.Symbol.Fill = new Fill(Color.Blue);
            //p3-avg
            LineItem myCurve3 = myPane.AddCurve(y_title +" Avg", points3_avg, Color.Orange, SymbolType.Default);
            myCurve3.Symbol.Size = 2;
            myCurve3.Symbol.Fill = new Fill(Color.Orange);
            //myPane.Xaxis.MajorGrid.Default.IsVisible = true;
            
        
            myPane.Chart.Fill = new Fill(Color.White, Color.LightSteelBlue, 35F);
            //myPane.
            myPane.Fill = new Fill(Color.White, Color.LightGray, 45F);
            myPane.YAxis.MajorGrid.IsVisible = true;

            myPane.YAxis.MinorGrid.IsVisible = true;

            myPane.XAxis.MajorGrid.IsVisible = true;

            myPane.XAxis.MinorGrid.IsVisible = true;
            g.zgc.AxisChange();
        }
        public void graphitall()
        {
            float s1 = 3;
            float s2 = 3;
            float s3 = 2;
            string y_title;
            graph g = new graph();
            g.Show();

            GraphPane myPane = g.zgc.GraphPane;
            myPane.Title.Text = "All Variables with Respect to Real Time";
            myPane.XAxis.Title.Text = "Time [Hours]";
            myPane.YAxis.Title.Text = "Unit [miles, mph, ft]";
            //p0
            y_title ="Speed [mph]";
            LineItem myCurve0 = myPane.AddCurve(y_title, listSandT, Color.Black, SymbolType.Star);
            myCurve0.Symbol.Size = s1;
            myCurve0.Symbol.Fill = new Fill(Color.Black);
            //p1-max
            LineItem myCurve1 = myPane.AddCurve(y_title + " Max", listSmax_andT, Color.DarkRed, SymbolType.Star);
            myCurve1.Symbol.Size = s1;
            myCurve1.Symbol.Fill = new Fill(Color.DarkRed);
            //p2-min
            LineItem myCurve2 = myPane.AddCurve(y_title + " Min", listSmin_andT, Color.DarkBlue, SymbolType.Star);
            myCurve2.Symbol.Size = s1;
            myCurve2.Symbol.Fill = new Fill(Color.DarkBlue);
            //p3-avg
            LineItem myCurve3 = myPane.AddCurve(y_title + " Avg", listSavg_andT, Color.DarkOrange, SymbolType.Star);
            myCurve3.Symbol.Size = s1;
            myCurve3.Symbol.Fill = new Fill(Color.DarkOrange);

            //p0
            y_title = "Altitude [ft]";
            LineItem myCurve00 = myPane.AddCurve(y_title, listAandT, Color.Black, SymbolType.Triangle);
            myCurve00.Symbol.Size = s2;
            myCurve00.Symbol.Fill = new Fill(Color.Gray);
            //p1-max
            LineItem myCurve01 = myPane.AddCurve(y_title + " Max", listAmax_andT, Color.Red, SymbolType.Triangle);
            myCurve01.Symbol.Size = s2;
            myCurve01.Symbol.Fill = new Fill(Color.Red);
            //p2-min
            LineItem myCurve02 = myPane.AddCurve(y_title + " Min", listAmin_andT, Color.Blue, SymbolType.Triangle);
            myCurve02.Symbol.Size = s2;
            myCurve02.Symbol.Fill = new Fill(Color.Blue);
            //p3-avg
            LineItem myCurve03 = myPane.AddCurve(y_title + " Avg", listAavg_andT, Color.Orange, SymbolType.Triangle);
            myCurve03.Symbol.Size = s2;
            myCurve03.Symbol.Fill = new Fill(Color.Orange);

            //p3-avg
            LineItem myCurve000 = myPane.AddCurve("Distance [miles]", listDandT, Color.Green, SymbolType.Circle);
            myCurve000.Symbol.Size = s3;
            myCurve000.Symbol.Fill = new Fill(Color.Green);

            // pane finalization
            myPane.Chart.Fill = new Fill(Color.LightGray, Color.SteelBlue, 45F);
            myPane.Fill = new Fill(Color.White, Color.LightGray, 25F);
            myPane.YAxis.MajorGrid.IsVisible = true;

            myPane.YAxis.MinorGrid.IsVisible = true;

            myPane.XAxis.MajorGrid.IsVisible = true;

            myPane.XAxis.MinorGrid.IsVisible = true;
            g.zgc.AxisChange();
        }

        private void graphAllExperimentalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphitall();
        }

        private void tableToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void showTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showtable();
        }

        private void saveXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveXML(tbl, false);
        }

        private void showTableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showtablef();
        }

        private void saveXMLFormattedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveXML(tblf, true);
        }

        private void latAndLongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphstuff(listLat_Long, "Map", "Longitude", "Latitude");
        }

    }
    class classDataPoint
    {
        public classDataPoint(int ID, string latitude, string longtitude, string altitude, string speed, int sec, int min, int hr, int day, int mo, int year, string acc, double deltaDfeet, double totalDmiles)
        {
            id = ID;
            this.Lat = latitude;
            this.Long = longtitude;
            this.Altitude = altitude;
            this.Speed = speed;
            this.Accuracy = acc;
            timeday1 = new DateTime(year, mo, day, hr, min, sec, 00, DateTimeKind.Unspecified);
            this.delta_d_ft = deltaDfeet;
            this.total_d_mi = totalDmiles;
        }
        private double lat1 = 0;
        private double long1 = 0;
        private double alt1 = 0;
        private double speed1 = 0;
        private double bearing1 = 0;
        private double accuracy = 0;
        //
        public double delta_d_ft;
        public double total_d_mi;
        public double alt_ft;
        public double speed_mph;
        //
        public int id;
        public DateTime timeday1;
        // - five 9s is bad error
        public string Accuracy
        {
            get { return accuracy.ToString(); }
            set
            {
                try
                {
                    accuracy = Convert.ToDouble(value);
                }
                catch (Exception)
                {
                    accuracy = -99999;
                }
            }
        }
        public string Lat
        {
            get { return lat1.ToString(); }
            set
            {
                try
                {
                    lat1 = Convert.ToDouble(value);
                }
                catch (Exception)
                {
                    lat1 = -99999;
                }
            }
        }
        public string Long
        {
            get { return long1.ToString(); }
            set
            {
                try
                {
                    long1 = Convert.ToDouble(value);
                }
                catch (Exception)
                {
                    long1 = -99999;
                }
            }
        }
        public string Bearing
        {
            get { return bearing1.ToString(); }
            set
            {
                try
                {
                    bearing1 = Convert.ToDouble(value);
                }
                catch (Exception)
                {
                    bearing1 = -99999;
                }
            }
        }
        public string Altitude
        {
            get { return alt1.ToString(); }
            set
            {
                try
                {
                    alt1 = Convert.ToDouble(value);
                    alt_ft = alt1 * 3.28084;
                }
                catch (Exception)
                {
                    alt1 = -99999;
                    alt_ft = -99999;
                }
            }
        }
        public string Speed
        {
            get { return speed1.ToString(); }
            set
            {
                try
                {
                    speed1 = Convert.ToDouble(value);
                    speed_mph = speed1 * 2.23694;
                }
                catch (Exception)
                {
                    speed1 = -99999;
                    speed_mph = -99999;
                }
            }
        }

    }
}
