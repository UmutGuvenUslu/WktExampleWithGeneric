import React, { useRef, useEffect, useState } from "react";
import axios from "axios";
import Map from "ol/Map";
import View from "ol/View";
import TileLayer from "ol/layer/Tile";
import VectorLayer from "ol/layer/Vector";
import VectorSource from "ol/source/Vector";
import OSM from "ol/source/OSM";
import WKT from "ol/format/WKT";
import { fromLonLat } from "ol/proj";
import { Style, Stroke, Fill, Circle as CircleStyle, Text } from "ol/style";

function MapView() {
  const mapRef = useRef(null);
  const mapInstance = useRef(null);

  const [data, setData] = useState([]);

  useEffect(() => {
    axios
      .get("https://localhost:7109/api/User/GetAll")
      .then((response) => setData(response.data))
      .catch((error) => console.error("API hatası:", error));
  }, []);

  const styleFunction = (feature) => {
    const geom = feature.getGeometry();
    const geomType = geom.getType();
    const idText = feature.getId()?.toString() || "";

    if (geomType === "Point") {
      return new Style({
        image: new CircleStyle({
          radius: 7,
          fill: new Fill({ color: "red" }),
          stroke: new Stroke({ color: "white", width: 2 }),
        }),
        text: new Text({
          text: idText,
          font: "12px Calibri,sans-serif",
          fill: new Fill({ color: "black" }),
          stroke: new Stroke({ color: "white", width: 3 }),
          scale: 1,
          overflow: true,
        }),
      });
    }

    if (geomType === "LineString") {
      return new Style({
        stroke: new Stroke({
          color: "blue",
          width: 3,
        }),
        text: new Text({
          text: idText,
          font: "12px Calibri,sans-serif",
          fill: new Fill({ color: "black" }),
          stroke: new Stroke({ color: "white", width: 3 }),
          scale: 1,
          placement: "line",
          overflow: true,
        }),
      });
    }

    if (geomType === "Polygon") {
      return new Style({
        stroke: new Stroke({
          color: "green",
          width: 2,
        }),
        fill: new Fill({
          color: "rgba(0, 128, 0, 0.3)",
        }),
        text: new Text({
          text: idText,
          font: "12px Calibri,sans-serif",
          fill: new Fill({ color: "black" }),
          stroke: new Stroke({ color: "white", width: 3 }),
          scale: 1,
          overflow: true,
        }),
      });
    }

    return new Style({
      stroke: new Stroke({
        color: "gray",
        width: 2,
      }),
    });
  };

  useEffect(() => {
    if (!mapRef.current) return;

    const wktFormat = new WKT();

    const features = (data || [])
      .map((item) => {
        try {
          const feature = wktFormat.readFeature(item.wkt, {
            dataProjection: "EPSG:4326",
            featureProjection: "EPSG:3857",
          });
          feature.setId(item.id);
          feature.set("name", item.name);
          return feature;
        } catch (e) {
          console.error("WKT parse hatası:", e, item.wkt);
          return null;
        }
      })
      .filter((f) => f !== null);

    const vectorSource = new VectorSource({
      features: features,
    });

    const vectorLayer = new VectorLayer({
      source: vectorSource,
      style: styleFunction,
      declutter: true,
    });

    if (!mapInstance.current) {
      mapInstance.current = new Map({
        target: mapRef.current,
        layers: [
          new TileLayer({
            source: new OSM(),
          }),
          vectorLayer,
        ],
        view: new View({
          center: fromLonLat([35, 39]),
          zoom: 6,
          projection: "EPSG:3857",
        }),
        controls: [], 
      });
    } else {
      const layers = mapInstance.current.getLayers();
      if (layers.getLength() > 1) {
        layers.setAt(1, vectorLayer);
      } else {
        mapInstance.current.addLayer(vectorLayer);
      }
    }
  }, [data]);

  return <div ref={mapRef} style={{ width: "100%", height: "400px" }} />;
}

export default MapView;
