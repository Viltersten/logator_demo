import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'datify'
})
export class DatifyPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return null;
  }

}
