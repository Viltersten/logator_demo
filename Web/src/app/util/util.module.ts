import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { UtilService } from "./util.service";
import { DatifyPipe } from "./datify.pipe";

export { UtilService } from "./util.service";

@NgModule({
  imports: [CommonModule],
  providers: [UtilService],
  declarations: [DatifyPipe]
})
export class UtilModule { }
