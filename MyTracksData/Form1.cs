﻿using System;
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

namespace MyTracksData
{

    public partial class Form1 : Form
    {
        List<classDataPoint> Points = new List<classDataPoint>();

        public Form1()
        {
            InitializeComponent();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main();

        }
        private void Main()
        {

            OpenFileDialog fopen = new OpenFileDialog();
            fopen.Multiselect = false;
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
            double rest_hours=0;
            total_travel_hr = 0;
            t1 = new DateTime();

            double mph_old = 0;


            for (int i = 0; i < splitlen; i++)
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

                    for (int j = 0; j < l1; j++)
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
                    rest_hours = total_time_hr.TotalHours - total_travel_hr;
                    if (mph > maxMPH) { maxMPH = mph; }
                    if (altft > maxALTft) { maxALTft = altft; }
                    if (mph < minMPH) { minMPH = mph; }
                    if (altft < minALTft) { minALTft = altft; }
                    sumMPH_data += mph;
                    avgMPH_data = sumMPH_data / iter_good;
                    sumALTft += altft;
                    avgALTft = sumALTft / iter_good;

                    avgMPH_map_real = total_d / total_time_hr.TotalHours; // take into account stops and stuff
                    avgMPH_map_travel = total_d / total_travel_hr;  // just when moving avg is calc
                   
                    //d=rt  so t=d/r
                    //END OF ANALYSIS STUFF


                    Points.Add(new classDataPoint(iter_good, slat, slong, salt, sspeed, second1, minute1, hour1, day1, month1, year1, sacc, delta_d_ft, total_d));


                    resstr = String.Format(" (Actual Pt/Total)  ({0}/{1}) = {2:00}:{3:00}:{4:00} {5:00}/{6:00}/{7:00}, Lat: {8:0.0000000}*, Long: {9:0.0000000}*, Alt: {10:0,0.000} m = {17:0.000} ft, Spd: {11:0.000} m/s = {16:0.000} mph, Br: {12:0.000}* --- ACC: {13} -- Delta_D: {14:0.000} ft, Total Distance: {15:0.000} mi", iter_good, i + 1, hour1, minute1, second1, month1, day1, year1, Convert.ToDouble(slat), Convert.ToDouble(slong), Convert.ToDouble(salt), Convert.ToDouble(sspeed), Convert.ToDouble(sbear), sacc, delta_d_ft, total_d, mph, altft);
                    // showing extra info
                    resstr +=Environment.NewLine;
                    resstr += String.Format("     INFO - min MPH: {0:0.000}, max MPH: {1:0.000}, avgMPH_data: {2:0.000}, avgMPH_map_real: {9:0.000}, avgMPH_map_travel: {10:0.000}, min ALT: {3:0,0.000}, max ALT: {4:0,0.000}, avg ALT: {5:0,0.000}, dTime_Travel(sec): {6:0.000}, Travel(hr): {7:0.000}, Real(hr): {8:0.000}, Rest(hr): {11:0.000}", minMPH, maxMPH, avgMPH_data, minALTft, maxALTft, avgALTft, (dTimeHigh + dTimeLow / 2) * 3600, total_travel_hr, total_time_hr.TotalHours,avgMPH_map_real,avgMPH_map_travel,rest_hours);
                    //  resstr = String.Format(" (Actual Pt/Total)  ({6}/{7}) = {7}:{8}:{9} {10}/{11}/{12}, Lat: {0}, Long: {1}, Alt: {2}, Spd: {3}, Brg: {4} --- ACC: {5}", slat, slong, salt, sspeed, sbear, sacc, iter_good, i + 1, second1, minute1, hour1, day1, month1, year1);
                    //   cwl("ERROR " + resstr);
                    res.Append(resstr + Environment.NewLine);
                    // rtb.AppendText(String.Format("{7}/{8} = t: {0}, Lat: {1}, Long: {2}, Alt: {3}, Spd: {4}, Brg: {5} --- ACC: {6}", stime, slat, slong, salt, sspeed, sbear, sacc, iter_good, i+1) + Environment.NewLine);



                }
            } // END I LOOP
            sw.Stop();
            rtb.AppendText("* FINISHED Analyzing & Formatting Info at " + DateTime.UtcNow + "-- Elapsed Time: " + sw.ElapsedMilliseconds + " ms" + Environment.NewLine); rtb.Refresh();
            sw.Restart();
            rtb.AppendText("* Going To Print Text (might take a moment):" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("====================================" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText(res.ToString());
            rtb.AppendText("====================================" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("* FINISHED Printing At " + DateTime.UtcNow + "-- Elapsed Time: " + sw.ElapsedMilliseconds + " ms" + Environment.NewLine); rtb.Refresh();
            rtb.AppendText("#######################" + Environment.NewLine); rtb.Refresh();
            sw.Reset();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { this.Close(); }
        private void Form1_Load(object sender, EventArgs e) { }
        private void cwl(string str) { Console.WriteLine(str); }
        private void cw(string str) { Console.Write(str); }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
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