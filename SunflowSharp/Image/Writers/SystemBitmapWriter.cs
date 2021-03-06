// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.IO;

namespace SunflowSharp.Image.Writers
{
	public class SystemBitmapWriter : BitmapWriter {

		private string filename;
	    private System.Drawing.Bitmap data;

		
		public override void configure(string option, string value) {
		}
		
		public override void openFile(string filename) {
			this.filename = filename;
		}
		
		public override void writeHeader(int width, int height, int tileSize) {

			data = new System.Drawing.Bitmap (width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

		}
		
		public override void writeTile(int x, int y, int w, int h, Color[] color, float[] alpha) {
			color = ColorEncoder.unlinearize(color); // gamma correction
			byte[] tileData = ColorEncoder.quantizeRGBA8(color, alpha);

			lock(data) {
				for (int j = 0, index = 0; j < h; j++) {
					for (int i = 0; i < w; i++, index += 4) {
						data.SetPixel(x + i, y + j, System.Drawing.Color.FromArgb(tileData[index + 3], 
						                                                          tileData[index], 
						                                                          tileData[index + 1], 
						                                                          tileData[index + 2]));

					}
				}
			}
		}
		
		public override void closeFile() {
			// actually write the file from here
			data.Save (filename);

		}
	}
}

