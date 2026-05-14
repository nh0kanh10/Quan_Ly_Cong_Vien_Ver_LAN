const html = require('fs').readFileSync('activity_diagrams.html','utf8');
const s = html.indexOf('<script>');
const e = html.indexOf('</script>');
const jsCode = html.substring(s + 8, e);

// Extract the object between the module definitions
const modStart = jsCode.indexOf('sanpham:');
const tabLogicStart = jsCode.indexOf('let curMod');
const objStr = '({' + jsCode.substring(modStart, tabLogicStart).replace(/;\s*\/\/[\s\S]*?$/, '') + '})';

try {
  const D = eval(objStr);
  console.log('Top-level keys:', Object.keys(D));
  for (const k of Object.keys(D)) {
    const mod = D[k];
    if (mod.tabs) {
      console.log(`  ${k}: tabs=[${mod.tabs.map(t=>t.id)}], diagrams=[${Object.keys(mod.diagrams)}]`);
    } else {
      console.log(`  ${k}: INVALID STRUCTURE`);
    }
  }
  console.log('\nALL OK!');
} catch(err) {
  console.log('PARSE ERROR:', err.message);
  // Find approximate line
  const lines = objStr.split('\n');
  const errMatch = err.message.match(/position (\d+)/);
  if (errMatch) {
    const pos = parseInt(errMatch[1]);
    let charCount = 0;
    for (let i = 0; i < lines.length; i++) {
      charCount += lines[i].length + 1;
      if (charCount >= pos) {
        console.log(`Near line ${i+1}: ${lines[i].trim().substring(0, 80)}`);
        break;
      }
    }
  }
}
