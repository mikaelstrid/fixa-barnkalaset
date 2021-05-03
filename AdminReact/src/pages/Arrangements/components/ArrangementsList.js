import React from "react";

function ArrangementsList(props) {
  return (
    <table className="table">
      <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col">Namn</th>
          <th scope="col">Stad</th>
        </tr>
      </thead>
      <tbody>
        {props.arrangements.map((a) => {
          return (
            <tr key={a.id}>
              <td>{a.id}</td>
              <td>{a.name}</td>
              <td>{a.city.name}</td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

export default ArrangementsList;
