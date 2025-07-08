import { useEffect, useState } from "react";
import axios from "axios";
import Add from "./Add";
import Update from "./Update";
import "./modal.css"; 

function Table() {
  const [mapObject, setMapObject] = useState([]);
  const [refresh, setRefresh] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [selectedItem, setSelectedItem] = useState(null);

  useEffect(() => {
    axios
      .get("https://localhost:7109/api/User/GetAll")
      .then((response) => setMapObject(response.data))
      .catch((error) => console.log(error));
  }, [refresh]);

  const Refresh = () => setRefresh((prev) => !prev);

  return (
    <>
      <table className="table">
        <thead>
          <tr>
            <th>#</th>
            <th>Id</th>
            <th>Name</th>
            <th>Wkt</th>
            <th></th>
            <th></th>
            <th className="text-center">
              <button className="btn btn-success" onClick={() => setShowModal(true)}>
                Ekle
              </button>
            </th>
          </tr>
        </thead>
        <tbody>
          {mapObject.map((object) => (
            <tr key={object.id}>
              <td></td>
              <td>{object.id}</td>
              <td>{object.name}</td>
              <td>{object.wkt}</td>
              <td>
                <button
                  className="btn btn-warning"
                  onClick={() => {
                    setSelectedItem(object);
                    setShowUpdateModal(true); 
                  }}
                >
                  Düzenle
                </button>
              </td>
              <td>
                <button
                  className="btn btn-danger"
                  onClick={() => {
                    axios
                      .delete(`https://localhost:7109/api/User/Delete?id=${object.id}`)
                      .then(() => {
                        console.log("Silme Başarılı");
                        Refresh();
                      })
                      .catch((error) => console.log(error));
                  }}
                >
                  Sil
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {showModal && (
        <div className="custom-modal-overlay p-0 m-0 fade-in">
          <div className="custom-modal-content p-0 m-0">
            <button className="close-button" onClick={() => setShowModal(false)}>
              &times;
            </button>
            <Add
              onClose={() => {
                setShowModal(false);
                Refresh();
              }}
            />
          </div>
        </div>
      )}

      {showUpdateModal && selectedItem && (
        <div className="custom-modal-overlay p-0 m-0 fade-in">
          <div className="custom-modal-content p-0 m-0">
            <button className="close-button" onClick={() => setShowUpdateModal(false)}>
              &times;
            </button>
            <Update
              data={selectedItem}
              onClose={() => {
                setShowUpdateModal(false);
                setSelectedItem(null);
                Refresh();
              }}
            />
          </div>
        </div>
      )}
    </>
  );
}

export default Table;
