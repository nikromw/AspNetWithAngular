import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
    selectedFile: File[];
  public  newFileForm: FormGroup;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.newFileForm = new FormGroup({
      Name: new FormControl(null),
      TileImage: new FormControl(null)
    });
  }

  onSelectFile(fileInput: any) {
    this.selectedFile = <File[]>fileInput.target.files;
  }

  onSubmit(data) {
    for (var i = 0; i < this.selectedFile.length; i++) {
      const formData = new FormData();
      formData.append('Content', this.selectedFile[i]);

      this.http.post('UploadFiles', formData)
        .subscribe(res => {

          alert('Uploaded!!');
        });

    }
    this.newFileForm.reset();
  }
}

