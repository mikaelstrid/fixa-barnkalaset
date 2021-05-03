export function getArrangements() {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      resolve([
        { id: 1, name: "Leksaksfabriken", city: { id: 1, name: "Lund" } },
        { id: 2, name: "Tut i luren", city: { id: 2, name: "Halmstad" } },
      ]);
    }, 300);
  });
}
