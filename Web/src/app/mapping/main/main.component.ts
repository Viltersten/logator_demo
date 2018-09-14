import { Component, OnInit, ViewChild } from "@angular/core";
import { } from "@types/googlemaps";
import { MappingService } from "../mapping.service";

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"]
})
export class MainComponent implements OnInit {

  constructor(private service: MappingService) { }

  @ViewChild("gspot")
  gspot: any;
  gmap: google.maps.Map;

  ngOnInit() {
    this.service.getMembers()
      .subscribe(next => {
        next = next.filter(_ => _.lat !== 0 || _.lng !== 0);

        const markers = next.map(_ => new google.maps.Marker({
          position: new google.maps.LatLng(_.lat, _.lng),
          map: this.gmap,
          animation: google.maps.Animation.DROP,
          title: _.name + ", " + _.address,
          icon: "assets/consid01.png"
        }));

        const bounds = new google.maps.LatLngBounds();
        markers.forEach(_ => bounds.extend(_.getPosition()));

        const properties = {
          clickableIcons: false,
          disableDefaultUI: true,
          zoomControl: true,
          mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        this.gmap = new google.maps.Map(this.gspot.nativeElement, properties);

        markers.forEach(_ => {
          _.addListener("click", () => {
            this.gmap.setCenter(_.getPosition());
          });
          _.addListener("mouseover", () => {
            // todo Navigate to the outlet with the popup.
            // this.gmap.setCenter(_.getPosition());
          });
          _.addListener("mouseout", () => {
            // todo Close the popup.
          });
        });

        markers.forEach(_ => _.setMap(this.gmap));
        this.gmap.fitBounds(bounds);
      });
  }
}
