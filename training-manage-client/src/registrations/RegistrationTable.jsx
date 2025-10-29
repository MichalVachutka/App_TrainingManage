// src/components/RegistrationTable.jsx
export default function RegistrationTable({ items, onDelete, columns }) {
  return (
    <table className="table">
      <thead>
        <tr>{columns.map(c => <th key={c.key}>{c.title}</th>)}</tr>
      </thead>
      <tbody>
        {items.map(r => (
          <tr key={r.id}>
            {columns.map(c => <td key={c.key}>{c.render(r)}</td>)}
            <td>
              <button onClick={() => onDelete(r.id)} className="btn btn-sm btn-danger">
                Odhlásit
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
