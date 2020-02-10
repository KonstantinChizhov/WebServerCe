﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WebServerCe;
using Newtonsoft.Json;
using System.IO;
using System.Globalization;

namespace WebServerTest
{
   
    public class DemoData : IQueryDispatcher
    {
        int[] Data = new int[] { 133, 32, 221, 799, 920, 686, 408, 251, 211, 228, 229, 149, -454, -1458, -1574, -1119, -666, -468, -470, -525, -488, -305, 647, 1544, 1476, 976, 573, 435, 459, 497, 411, 0, -1095, -1550, -1220, -722, -434, -380, -422, -416, -309, 28, 618, 713, 477, 237, 136, 154, 190, 192, 176, 197, 262, 337, 368, 322, 193, 13, -155, -288, -402, -503, -586, -596, -461, -144, 324, 875, 1302, 1501, 1462, 1246, 906, 438, -211, -1086, -2147, -3112, -3648, -3562, -2851, -1731, -448, 841, 2110, 3414, 4647, 5502, 5657, 4981, 3613, 1884, 100, -1592, -3303, -4976, -6379, -7160, -7022, -5968, -4276, -2338, -430, 1378, 3091, 4738, 6029, 6634, 6351, 5313, 3864, 2388, 1080, -100, -1194, -2263, -3123, -3610, -3706, -3557, -3356, -3189, -2961, -2531, -1849, -1029, -200, 541, 1288, 2041, 2861, 3627, 4148, 4296, 4044, 3472, 2626, 1570, 320, -976, -2238, -3315, -4083, -4502, -4561, -4250, -3515, -2357, -931, 437, 1468, 2023, 2241, 2323, 2415, 2517, 2453, 2098, 1530, 1025, 825, 944, 1121, 1085, 680, -48, -893, -1783, -2633, -3471, -4245, -4738, -4646, -3821, -2341, -490, 1376, 2985, 4304, 5296, 5873, 5841, 4981, 3303, 1096, -1183, -3145, -4540, -5281, -5428, -4984, -3883, -2130, 36, 2158, 3865, 4930, 5261, 4874, 3879, 2375, 467, -1624, -3605, -4993, -5487, -5087, -4081, -2819, -1545, -318, 905, 2115, 3146, 3725, 3660, 3056, 2252, 1594, 1196, 982, 758, 413, 2, -333, -533, -643, -773, -1006, -1360, -1793, -2207, -2523, -2680, -2635, -2327, -1698, -772, 313, 1364, 2143, 2634, 2849, 2864, 2700, 2309, 1657, 779, -137, -810, -1116, -986, -549, -15, 481, 845, 1007, 923, 571, -52, -907, -2007, -3142, -4065, -4565, -4516, -3907, -2807, -1323, 419, 2252, 4043, 5591, 6665, 7120, 6913, 6079, 4727, 2923, 751, -1655, -4151, -6368, -7808, -8234, -8148, -7680, -5934, -3677, -1282, 1052, 3130, 4898, 6164, 6794, 6759, 6139, 5112, 3888, 2623, 1408, 226, -888, -1930, -2863, -3642, -4253, -4682, -4904, -4872, -4527, -3832, -2855, -1739, -594, 551, 1728, 2907, 4068, 4990, 5441, 5307, 4707, 3891, 3056, 2234, 1331, 248, -930, -2062, -2931, -3409, -3575, -3657, -3816, -4025, -4107, -3893, -3327, -2495, -1572, -674, 207, 1213, 2355, 3570, 4605, 5240, 5419, 5230, 4832, 4277, 3491, 2368, 896, -738, -2262, -3465, -4272, -4757, -5030, -5122, -4970, -4494, -3693, -2702, -1677, -718, 186, 1115, 2055, 2961, 3741, 4326, 4709, 4924, 4930, 4610, 3870, 2708, 1294, -165, -1479, -2676, -3730, -4607, -5177, -5265, -4792, -3838, -2650, -1475, -431, 492, 1368, 2124, 2711, 2985, 2896, 2516, 2008, 1514, 1058, 589, 90, -382, -758, -937, -862, -571, -175, 200, 554, 828, 1010, 1048, 888, 486, -135, -889, -1691, -2344, -2724, -2771, -2495, -1952, -1233, -424, 385, 1163, 1765, 2156, 2297, 2192, 1881, 1450, 951, 465, 53, -225, -339, -293, -96, 244, 678, 1107, 1447, 1614, 1500, 1006, 82, -1149, -2625, -4090, -5306, -6063, -6183, -5582, -4233, -2271, 56, 2411, 4575, 6394, 7587, 7902, 7857, 7622, 6329, 4144, 1630, -885, -3173, -5114, -6590, -7535, -7876, -7620, -6668, -5166, -3368, -1571, -1, 1250, 2132, 2724, 3094, 3310, 3470, 3660, 3935, 4266, 4549, 4636, 4419, 3834, 2864, 1535, -109, -1914, -3776, -5446, -6730, -7488, -7660, -7251, -6278, -4821, -2996, -1005, 938, 2631, 4080, 5243, 6090, 6582, 6621, 6148, 5231, 4067, 2861, 1733, 676, -410, -1555, -2760, -3840, -4628, -5099, -5352, -5464, -5390, -4971, -4097, -2774, -1179, 399, 1740, 2777, 3632, 4313, 4730, 4755, 4341, 3585, 2717, 1985, 1495, 1139, 747, 273, -153, -418, -507, -576, -855, -1521, -2509, -3568, -4435, -5011, -5361, -5552, -5503, -4989, -3811, -1940, 413, 2812, 5000, 6787, 7806, 7982, 7924, 7813, 7604, 6209, 3586, 701, -1989, -4412, -6526, -7944, -8322, -8277, -8166, -7974, -7307, -5054, -2545, -8, 2510, 4970, 7042, 7988, 8083, 7974, 7504, 5760, 3818, 1885, -9, -1802, -3406, -4526, -4999, -4861, -4325, -3624, -2899, -2129, -1239, -228, 769, 1569, 2016, 2104, 1879, 1422, 817, 121, -576, -1207, -1600, -1600, -1184, -449, 398, 1217, 1842, 2290, 2554, 2597, 2357, 1815, 1017, 100, -709, -1340, -1745, -1973, -2099, -2135, -2028, -1712, -1187, -544, 32, 412, 538, 446, 271, 118, 44, 30, 35, 67, 192, 512, 1011, 1522, 1870, 1941, 1739, 1381, 988, 604, 183, -324, -917, -1491, -1839, -1844, -1544, -1083, -651, -362, -215, -112, -11, 109, 105, -97, -461, -880, -1122, -1039, -646, -90, 476, 1016, 1501, 1967, 2422, 2782, 2924, 2760, 2294, 1634, 894, 124, -647, -1479, -2324, -3065, -3577, -3777, -3691, -3401, -2960, -2372, -1600, -689, 259, 1163, 1919, 2561, 3113, 3557, 3826, 3837, 3527, 2941, 2203, 1456, 757, 108, -488, -1085, -1668, -2175, -2572, -2882, -3157, -3404, -3560, -3514, -3173, -2530, -1665, -671, 374, 1459, 2503, 3473, 4200, 4520, 4384, 3876, 3172, 2426, 1701, 959, 99, -830, -1768, -2526, -2971, -3096, -3020, -2847, -2604, -2228, -1687, -1027, -395, 61, 312, 464, 612, 821, 1035, 1200, 1322, 1466, 1703, 2030, 2349, 2468, 2270, 1767, 1103, 428, -171, -715, -1368, -2112, -2855, -3396, -3560, -3318, -2771, -2069, -1288, -444, 446, 1367, 2157, 2748, 3032, 3010, 2762, 2376, 1904, 1352, 697, -18, -692, -1274, -1644, -1767, -1716, -1593, -1439, -1218, -866, -363, 183, 641, 812, 666, 344, 54, -63, -44, -16, -118, -366, -677, -821, -659, -208, 357, 900, 1291, 1569, 1827, 2110, 2328, 2301, 1890, 1101, 62, -971, -1923, -2674, -3203, -3507, -3550, -3312, -2809, -2126, -1365, -605, 136, 953, 1834, 2755, 3621, 4247, 4517, 4419, 4030, 3408, 2561, 1458, 102, -1356, -2775, -3908, -4633, -4966, -4967, -4681, -4116, -3287, -2266, -1142, -21, 1084, 2142, 3177, 4079, 4664, 4770, 4367, 3581, 2617, 1676, 836, 73, -607, -1226, -1699, -1933, -1944, -1847, -1805, -1939, -2249, -2611, -2869, -2910, -2695, -2230, -1544, -677, 319, 1404, 2446, 3424, 4221, 4754, 4980, 4876, 4422, 3614, 2473, 1092, -404, -1850, -3182, -4290, -5118, -5600, -5646, -5231, -4395, -3267, -2017, -785, 342, 1382, 2276, 3065, 3688, 4104, 4311, 4334, 4200, 3899, 3399, 2667, 1719, 621, -512, -1607, -2678, -3684, -4575, -5231, -5522, -5349, -4730, -3788, -2642, -1386, -63, 1288, 2571, 3743, 4641, 5118, 5138, 4765, 4136, 3393, 2612, 1792, 905, -52, -975, -1849, -2601, -3255, -3832, -4306, -4579, -4551, -4178, -3489, -2564, -1481, -292, 937, 2100, 3120, 3900, 4358, 4492, 4351, 3992, 3456, 2738, 1834, 770, -359, -1409, -2301, -2956 };

        #region IQueryDispatcher Members
   
        public bool ProcessQuery(SimpleWebRequest request, HttpResponse response)
        {
            object result = null;
            if (request.Path.Equals("/mayapp/api/v1/data.json", StringComparison.InvariantCultureIgnoreCase))
            {
                result = Data;
            }

            if (result != null)
            {
                string mimeType = "application/json";
                Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
                StringWriter writer = new StringWriter();
                ser.Serialize(writer, result);
                string data = writer.GetStringBuilder().ToString();
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                MemoryStream ms = new MemoryStream(buffer);
                response.SendFile(HttpStatus.OK, ms, mimeType);
                return true;
            }
            return false;
        }

        #endregion
    }
}
