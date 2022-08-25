<script setup lang="ts">
import { ref } from 'vue';

const url = ref(`/api/test`);
const output = ref("");

async function doFetch(): Promise<void> {
    if (!url.value || !url.value.trim()) {
        return;
    }

    try {
        var response = await fetch(url.value);
        output.value += `${response.status} ${response.statusText}\r\n`;
        output.value += `${JSON.stringify(await response.json(), null, 2)}\r\n`;
        output.value += `\r\n`;
    } catch (err) {
        alert(err);
    }
}

function clearOutput(): void {
    output.value = "";
}
</script>

<template>
    <div class="console">
        <div class="header">URL (<a href="/api/swagger">API Swagger</a>)</div>
        <div class="fetch">
            <input class="fetch__input" v-model="url">
            <button class="fetch__btn" @click="doFetch">Fetch</button>
        </div>

        <div class="header">Output</div>
        <textarea class="output__text" readonly v-model="output"></textarea>
        <button class="output__clear-btn" @click="clearOutput">Clear</button>
    </div>
</template>

<style>
.console {
    display: flex;
    flex-direction: column;
    width: 640px;
}

.header {
    margin: 14px 0 4px 8px;
}

.fetch {
    display: flex;
}

.fetch__input {
    width: 100%;
    border: 0;
    font-size: 15px;
    background: #1E1E1E;
    color: whitesmoke;
    padding: 4px 8px;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.fetch__btn {
    padding: 6px 12px;
    margin-left: 16px;
}

.output__text {
    border: 0;
    height: 480px;
    resize: none;
    font-size: 15px;
    background: #1E1E1E;
    color: whitesmoke;
    padding: 4px 8px;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.output__clear-btn {
    padding: 6px 12px;
    margin-top: 8px;
    align-self: flex-end;
}

:link { color: white; }
:visited { color: white; }
:link:active, :visited:active { color: #FF0000; }
:link, :visited { text-decoration: underline; cursor: pointer; }
</style>