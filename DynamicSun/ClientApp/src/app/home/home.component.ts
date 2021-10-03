import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
    selectedFile: File = null;
  public  newFileForm: FormGroup;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.newFileForm = new FormGroup({
      Name: new FormControl(null),
      TileImage: new FormControl(null)
    });
  }

  onSelectFile(fileInput: any) {
    this.selectedFile = <File>fileInput.target.files[0];
  }

  onSubmit(data) {

    const formData = new FormData();
    formData.append('Name', data.Name);
    formData.append('Content', this.selectedFile);

    this.http.post('UploadFiles', formData)
      .subscribe(res => {

        alert('Uploaded!!');
      });

    this.newFileForm.reset();
  }
}

