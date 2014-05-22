using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class ShellTileUtil
    {
        /// <summary>
        ///   添加桌面快连，返回0代表失败，返回1代表创建成功，返回2代表更新成功
        /// </summary>
        public static int CreateTile(string strNavPara, string strTitle, string uriBgImg = null, int nCount = 0,
                        string strBackTitle = null, string uriBackBgImg = null, string uriBackContent = null)
        {
            if (string.IsNullOrEmpty(strNavPara))
                return 0;

            // Look to see if the tile already exists and if so, don't try to create again.
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().CompareTo(strNavPara) == 0);
            // Create the tile if we didn't find it already exists.

            StandardTileData NewTileData = new StandardTileData();
            NewTileData.Title = strTitle;
            if (string.IsNullOrEmpty(uriBgImg) == false)
                NewTileData.BackgroundImage = new Uri(uriBgImg, UriKind.RelativeOrAbsolute);
            if (nCount > 0)
                NewTileData.Count = nCount;
            if (string.IsNullOrEmpty(strBackTitle) == false)
                NewTileData.BackTitle = strBackTitle;
            if (string.IsNullOrEmpty(uriBackBgImg) == false)
                NewTileData.BackgroundImage = new Uri(uriBackBgImg, UriKind.RelativeOrAbsolute);
            if (string.IsNullOrEmpty(uriBackContent) == false)
                NewTileData.BackContent = uriBackContent;

            // Create the tile and pin it to Start. This will cause a navigation to Start and a deactivation of our application.
            if (TileToFind != null)
            {
                TileToFind.Update(NewTileData);
                return 2;
            }
            else
            {
                ShellTile.Create(new Uri(strNavPara, UriKind.Relative), NewTileData);
                return 1;
            }
        }

        /// <summary>
        ///   删除桌面快连
        /// </summary>
        public static void DltTile(string strNavPara)
        {
            if (string.IsNullOrEmpty(strNavPara))
                return;

            // Look to see if the tile already exists and if so, don't try to create again.
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().CompareTo(strNavPara) == 0);
            // Create the tile if we didn't find it already exists.
            if (TileToFind != null)
                TileToFind.Delete();
            return;
        }

        /// <summary>
        ///   删除桌面快连
        /// </summary>
        public static bool IsExsitTile(string strNavPara)
        {

            // Look to see if the tile already exists and if so, don't try to create again.
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().CompareTo(strNavPara) == 0);
            // Create the tile if we didn't find it already exists.
            if (TileToFind != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
