import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { MainComponent } from "./main/main.component";
import { UpdateComponent } from "./update/update.component";
import { ListComponent } from "./list/list.component";
import { ScheduleComponent } from "./schedule/schedule.component";
import { MappingService } from "./mapping.service";

export { MainComponent } from "./main/main.component";
export { UpdateComponent } from "./update/update.component";
export { ListComponent } from "./list/list.component";
export { ScheduleComponent } from "./schedule/schedule.component";
export { MappingService } from "./mapping.service";

@NgModule({
  imports: [CommonModule, ReactiveFormsModule],
  providers: [MappingService],
  declarations: [MainComponent, UpdateComponent, ListComponent, ScheduleComponent]
})
export class MappingModule { }
