import { Pipe, PipeTransform } from '@angular/core';
import { Config } from '../models/config';

@Pipe({
  name: 'position',
  standalone: true,
})
export class PositionPipe implements PipeTransform {

  transform(list: Config[], position: string): Config[] {
    return list.filter(x => x.position == position);
  }

}
