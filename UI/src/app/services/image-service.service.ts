import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ImageService {
  private imageData: { [key: string]: string } = {};

  saveImage(fileName: string, imageUrl: string) {
    this.imageData[fileName] = imageUrl;
  }
  saveImageToAssetsDirectory(dataURL: string, filename: string) {
    const link = document.createElement('a');
    link.href = dataURL;
    link.download = filename;

    // Trigger a click event on the link to initiate the download.
    link.click();
  }
  getImage(fileName: string): string | undefined {
    return this.imageData[fileName];
  }
}
